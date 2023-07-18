using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SchoolOfDevs.Controllers;
using SchoolOfDevs.Dto.Note;
using SchoolOfDevs.Entities;
using SchoolOfDevs.Exceptions;
using SchoolOfDevs.Helpers;

namespace SchoolOfDevs.Services
{
    public interface INoteService
    {
        public Task<NoteResponse> Create(NoteRequest note);
        public Task<NoteResponse> GetById(int id);
        public Task<List<NoteResponse>> GetAll();
        public Task Update(NoteRequest NoteIn, int id);
        public Task Delete(int id);
    }
    public class NoteService : INoteService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public NoteService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<NoteResponse> Create(NoteRequest noteRequest)
        {
            Note note = _mapper.Map<Note>(noteRequest);

            _context.Notes.Add(note);
            await _context.SaveChangesAsync();

            return _mapper.Map<NoteResponse>(note);
        }

        public async Task Delete(int id)
        {
            Note noteDb = await _context.Notes.
                SingleOrDefaultAsync(u => u.Id == id);

            if (noteDb is null)
                throw new KeyNotFoundException($"Note {id} not found");

            _context.Notes.Remove(noteDb);
            await _context.SaveChangesAsync();
        }

        public async Task<List<NoteResponse>> GetAll()
        {
            List<Note> notes = await _context.Notes.ToListAsync();

            return notes.Select(n => _mapper.Map<NoteResponse>(n)).ToList();
        }

        public async Task<NoteResponse> GetById(int id)
        {
            Note noteDb = await _context.Notes.
                SingleOrDefaultAsync(u => u.Id == id);

            if (noteDb is null)
                throw new KeyNotFoundException($"Note {id} not found");

            return _mapper.Map<NoteResponse>(noteDb);
        }

        public async Task Update(NoteRequest noteRequest, int id)
        {
            if (noteRequest.Id != id)
                throw new BadRequestException("Route is different from Note id");

            Note noteDb = await _context.Notes
                .AsNoTracking()
                .SingleOrDefaultAsync(u => u.Id == id);

            if (noteDb is null)
                throw new KeyNotFoundException($"Note {id} not found");

            noteDb = _mapper.Map<Note>(noteRequest);

            _context.Entry(noteDb).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}