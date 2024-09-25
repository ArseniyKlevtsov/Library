using AutoMapper;
using Library.Application.DTOs.OrderDtos.Request;
using Library.Application.DTOs.OrderDtos.Respose;
using Library.Application.Exceptions;
using Library.Application.Interfaces.UseCases.OrderUseCases;
using Library.Domain.Entities;
using Library.Domain.Interfaces.Services;
using Library.Infrastructure;

namespace Library.Application.UseCases.OrderUseCases;

public class PlaceOrder: IPlaceOrder
{
    private readonly UnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IJwtTokenService _tokenService;

    public PlaceOrder(UnitOfWork unitOfWork, IMapper mapper, IJwtTokenService tokenService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _tokenService = tokenService;
    }

    public async Task<OrderResponseDto> ExecuteAsync(PlaceOrderRequestDto placeOrderRequestDto, CancellationToken cancellationToken)
    {
        var user = await _tokenService.GetUserFromTokenAsync(placeOrderRequestDto.AccessToken!);
        if (user == null)
        {
            throw new NotFoundException("the user transferred in the token was not found");
        }
        var rentedBooks = _mapper.Map<ICollection<RentedBook>>(placeOrderRequestDto.RentedBooks);
        await CheckAvailableAsync(rentedBooks, cancellationToken);
        await SubtractCountsAsync(rentedBooks, cancellationToken);
        SetReturnTime(rentedBooks);
        var rentOrder = new RentOrder()
        {
            UserId = user!.Id,
            RentedBooks = rentedBooks

        };
        await _unitOfWork.RentOrders.AddAsync(rentOrder, cancellationToken);
        await _unitOfWork.SaveAsync();

        return _mapper.Map<OrderResponseDto>(rentOrder);
    }

    private void SetReturnTime(ICollection<RentedBook> rentedBooks)
    {
        var returnTime = DateTime.Now.AddDays(30);
        foreach (var rentedBook in rentedBooks)
        {
            rentedBook.ReturnTime = returnTime;
        }
    }

    private async Task<bool> CheckAvailableAsync(ICollection<RentedBook> rentedBooks, CancellationToken cancellationToken)
    {
        var inventoryItems = await _unitOfWork.LibraryInventorys.GetAllAsync(cancellationToken);

        foreach (var rentedBook in rentedBooks)
        {
            var inventoryItem = inventoryItems.FirstOrDefault(i => i.BookId == rentedBook.BookId);

            if (inventoryItem != null && inventoryItem.AvailableCount < rentedBook.BooksCount)
            {
                throw new NoAvailableQuantityException($"There are not enough books for {rentedBook.BookId}. Available: {inventoryItem.AvailableCount}, requested: {rentedBook.BooksCount}.");
            }
        }

        return true;
    }
    private async Task SubtractCountsAsync(ICollection<RentedBook> rentedBooks, CancellationToken cancellationToken)
    {
        var inventoryItems = await _unitOfWork.LibraryInventorys.GetAllAsync(cancellationToken);

        foreach (var rentedBook in rentedBooks)
        {
            var inventoryItem = inventoryItems.FirstOrDefault(i => i.BookId == rentedBook.BookId);
            inventoryItem!.AvailableCount -= rentedBook.BooksCount;
            await _unitOfWork.LibraryInventorys.UpdateAsync(inventoryItem, cancellationToken);
        }
    }
}
