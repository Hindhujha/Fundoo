﻿using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepositoryLayer.Interface;
using Microsoft.EntityFrameworkCore;
using CommonLayer.LabelModel;
using CommonLayer.Label;
using CommonLayer.Notes;

namespace RepositoryLayer.Services
{
    public class LabelRL : ILabelRL
    {
        FundooDbContext dbContext;

        public LabelRL(FundooDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<Label>> CreateLabel(LabelPostModel labelModel, int NotesId, int UserId)
        {
            try
            {
                var user = dbContext.User.FirstOrDefault(e => e.UserId == UserId);
                var note = dbContext.Note.FirstOrDefault(u => u.NotesId == NotesId);
                Label label = new Label();
                label.UserId = UserId;
                label.NotesId = NotesId;
                label.LabelId = new Label().LabelId;
                label.LabelName = labelModel.LabelName;
                label.User = user;
                label.Note = note;
                dbContext.Label.Add(label);
                await dbContext.SaveChangesAsync();
                return await dbContext.Label.Where(u => u.UserId == UserId)
                    .Include(u => u.Note)
                    .Include(u => u.User)
                    .ToListAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

      
        public bool UpdateLabel(int LabelId, LabelPostModel labelPost)
        {
            Label label = dbContext.Label.Where(e => e.LabelId == LabelId).FirstOrDefault();         
            label.LabelName = labelPost.LabelName;
            dbContext.Label.Update(label);
            var result = dbContext.SaveChangesAsync();
            if (result != null)
                return true;
            else
                return false;

        }


        public bool DeleteLabel(int LabelId)
        {
            Label label = dbContext.Label.Where(e => e.LabelId == LabelId).FirstOrDefault();
            if (label != null)
            {
                dbContext.Label.Remove(label);
                dbContext.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<List<LabelResponse>> GetAllDatas(int userId)
        {
            Label labels = new Label();
            try
            {
                //return await dbContext.Label.Where(l => l.UserId == userId)
                //  //.Include(u => u.Note)
                //  //.Include(u => u.User)
                //  //.ToListAsync();
                //  .Join(dbContext.User,
                //l => l.UserId,
                //u => u.UserId,
                //(l, u) => new LabelResponse
                //{
                //    userId = (int)l.UserId,
                //    email = u.email,
                //    LabelName = l.LabelName,
                //    fName = u.fName,
                //    lName = u.lName,
                //}).ToListAsync();
                return await dbContext.Label.Where(l => l.UserId == userId)

                  .Join(dbContext.User
                  .Join(dbContext.Note,
                    u => u.UserId,
                    n => n.UserId,
                    (u, n) => new NoteResponse
                    {
                       
                        Title = n.Title,
                        Description = n.Description

                    }),
                   l => l.LabelName,
                    n1 => n1.LabelName,
                    (l, n1) => new LabelResponse
                    {
                        
                        // User=l.User.UserId,
                        fName=l.User.fName,
                        color = n1.color,
                        LabelName = l.LabelName,

                    }).ToListAsync();


            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<List<Label>> GetAllLabelsByNoteId(int NoteId,int UserId)
        {

            return await dbContext.Label.Where(u => u.UserId == UserId && u.NotesId == NoteId)

               .Include(u => u.User)
               .Include(u=>u.Note)
               .ToListAsync();
        }

    }

}
