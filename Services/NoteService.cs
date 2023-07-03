﻿using Microsoft.EntityFrameworkCore;
using SchoolOfDevs.Controllers;
using SchoolOfDevs.Entities;
using SchoolOfDevs.Helpers;

namespace SchoolOfDevs.Services
{
    public interface INoteService
    {
        public Task<Note> Create(Note note);
        public Task<Note> GetById(int id);
        public Task<List<Note>> GetAll();
        public Task Update(Note NoteIn, int id);
        public Task Delete(int id);
    }
    public class NoteService : INoteService
    {
        private readonly DataContext _context;

        public NoteService(DataContext context)
        {
            _context = context;
        }
        public async Task<Note> Create(Note note)
        {
            _context.Notes.Add(note);
            await _context.SaveChangesAsync();

            return note;
        }

        public async Task Delete(int id)
        {
            Note noteDb = await _context.Notes.
                SingleOrDefaultAsync(u => u.Id == id);

            if (noteDb is null)
                throw new Exception($"Note {id} not found");

            _context.Notes.Remove(noteDb);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Note>> GetAll() => await _context.Notes.ToListAsync();

        public async Task<Note> GetById(int id)
        {
            Note noteDb = await _context.Notes.
                SingleOrDefaultAsync(u => u.Id == id);

            if (noteDb is null)
                throw new Exception($"Note {id} not found");

            return noteDb;
        }

        public async Task Update(Note noteIn, int id)
        {
            if (noteIn.Id != id)
                throw new Exception("Route is different from Note id");

            Note noteDb = await _context.Notes
                .AsNoTracking()
                .SingleOrDefaultAsync(u => u.Id == id);

            if (noteDb is null)
                throw new Exception($"Note {id} not found");

            _context.Entry(noteIn).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}