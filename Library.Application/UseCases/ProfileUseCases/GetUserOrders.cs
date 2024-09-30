using AutoMapper;
using Library.Application.DTOs.RentedBookDtos.Request;
using Library.Application.DTOs.RentedBookDtos.Response;
using Library.Application.Exceptions;
using Library.Application.Interfaces.UseCases.ProfileUseCases;
using Library.Domain.Entities;
using Library.Domain.IncludeStates;
using Library.Domain.Interfaces;
using Library.Domain.Interfaces.Services;
using System.Linq.Expressions;

namespace Library.Application.UseCases.ProfileUseCases;

public class GetUserOrders: IGetUserOrders
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IJwtTokenService _tokenService;

    public GetUserOrders(IUnitOfWork unitOfWork, IMapper mapper, IJwtTokenService tokenService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _tokenService = tokenService;
    }

    public async Task<IEnumerable<RentedBookProfileResponseDto>> ExecuteAsync(GetUserOrdersRequestDto getUserOrdersDto, CancellationToken cancellationToken)
    {
        var user = await _tokenService.GetUserFromTokenAsync(getUserOrdersDto.AccessToken!);
        if (user == null)
        {
            throw new NotFoundException("the user transferred in the token was not found");
        }
        var includeState = new RentedBookIncludeState()
        {
            IncludeBook = true,
            IncludeRentOrder = true,
        };
        Expression<Func<RentedBook,bool>> predicate = rentedBook => rentedBook!.RentOrder!.UserId == user.Id;

        var userRentedBooks = await _unitOfWork.RentedBooks.GetWithIncludeByPredicateAsync(predicate,includeState, cancellationToken);

        return _mapper.Map<IEnumerable<RentedBookProfileResponseDto>>(userRentedBooks);
    }
}
