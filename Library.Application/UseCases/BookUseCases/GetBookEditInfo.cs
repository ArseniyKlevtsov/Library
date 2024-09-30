using AutoMapper;
using Library.Application.DTOs.AuthorDtos.Response;
using Library.Application.DTOs.BookDtos.Response;
using Library.Application.DTOs.GenreDtos.Response;
using Library.Application.Interfaces.UseCases.BookUseCases;
using Library.Domain.Interfaces;

namespace Library.Application.UseCases.BooksUseCases;

public class GetBookEditInfo : IGetBookEditInfo
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetBookEditInfo(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<BookEditInfo> ExecuteAsync(CancellationToken cancellationToken)
    {
        var authors = await _unitOfWork.Authors.GetAllAsync(cancellationToken);
        var genres = await _unitOfWork.Genres.GetAllAsync(cancellationToken);

        var bookEditInfo = new BookEditInfo()
        {
            Authors = _mapper.Map<IEnumerable<AuthorResponseDto>>(authors),
            Genres = _mapper.Map<IEnumerable<GenreResponseDto>>(genres)
        };

        return bookEditInfo;
    }
}
