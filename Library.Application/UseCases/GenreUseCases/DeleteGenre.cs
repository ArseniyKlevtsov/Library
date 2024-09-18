using AutoMapper;
using Library.Application.Exceptions;
using Library.Application.Interfaces.UseCases.GenreUseCases;
using Library.Infrastructure;

namespace Library.Application.UseCases.GenresUseCases;

public class DeleteGenre : IDeleteGenre
{
    private readonly UnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public DeleteGenre(UnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task ExecuteAsync(Guid id, CancellationToken cancellationToken)
    {
        var genre = await _unitOfWork.Genres.GetByIdAsync(id, cancellationToken);
        if (genre == null)
        {
            throw new NotFoundException($"Genre with ID {id} not found");
        }
        await _unitOfWork.Genres.DeleteAsync(genre, cancellationToken);
        return;
    }
}
