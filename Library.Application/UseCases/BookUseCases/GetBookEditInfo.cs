using AutoMapper;
using Library.Application.DTOs.BookDtos.Response;
using Library.Application.Interfaces.UseCases.BookUseCases;
using Library.Infrastructure;

namespace Library.Application.UseCases.BooksUseCases;

public class GetBookEditInfo : IGetBookEditInfo
{
    private readonly UnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetBookEditInfo(UnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<BookEditInfo> ExecuteAsync(CancellationToken cancellationToken)
    {
        var authors = await _unitOfWork.Authors.GetAllAsync(cancellationToken);
        var genres = await _unitOfWork.Genres.GetAllAsync(cancellationToken);

        return new BookEditInfo() { Authors = authors, Genres = genres };
    }
}
