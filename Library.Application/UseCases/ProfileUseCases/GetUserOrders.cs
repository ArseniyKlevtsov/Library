using AutoMapper;
using Library.Application.DTOs.RentedBookDtos.Request;
using Library.Application.DTOs.RentedBookDtos.Response;
using Library.Application.Interfaces.Services;
using Library.Application.Interfaces.UseCases.ProfileUseCases;
using Library.Domain.Entities;
using Library.Domain.IncludeStates;
using Library.Infrastructure;
using System.Linq.Expressions;

namespace Library.Application.UseCases.ProfileUseCases;

public class GetUserOrders: IGetUserOrders
{
    private readonly UnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ITokenService _tokenService;

    public GetUserOrders(UnitOfWork unitOfWork, IMapper mapper, ITokenService tokenService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _tokenService = tokenService;


    }

    public async Task<IEnumerable<RentedBookProfileResponseDto>> ExecuteAsync(GetUserOrdersRequestDto getUserOrdersDto, CancellationToken cancellationToken)
    {
        var user = await _tokenService.GetUserFromTokenAsync(getUserOrdersDto.AccessToken!);

        var includeState = new RentedBookIncludeState()
        {
            IncludeBook = true,
            IncludeRentOrder = true,
        };
        Expression<Func<RentedBook,bool>> predicate = rentedBook => rentedBook!.RentOrder!.UserId == user!.Id;

        var userRentedBooks = await _unitOfWork.RentedBooks.GetWithIncludeByPredicateAsync(predicate,includeState, cancellationToken);

        return _mapper.Map<IEnumerable<RentedBookProfileResponseDto>>(userRentedBooks);
    }
}
