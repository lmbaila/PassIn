﻿using PassIn.Communication.Requests;
using PassIn.Communication.Responses;
using PassIn.Exceptions;
using PassIn.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassIn.Application.UseCases.Events.Register
{
    public class RegisterEventUseCase
    {
        public ResponseRegisteredEventJson Execute(RequestEventJson request)
        {
            Validate(request);

            var dbContext = new PassInDbContext();
            var entity = new Infrastructure.Entities.Event
            { 
                Title = request.Title,
                Details = request.Details,
                Maximum_Attendees = request.MaximumAttendees,
                Slug = request.Title.ToLower().Replace(" ", "-"),
   
            };

            dbContext.Events.Add(entity);
            dbContext.SaveChanges();

            return new ResponseRegisteredEventJson
            {
                Id = entity.Id
            };

        }

        private void Validate(RequestEventJson request)
        {
            if(request.MaximumAttendees <= 0)
            {
                throw new PassInException("The Maximum attendes is invalid.");
            }

            if (string.IsNullOrWhiteSpace(request.Title)) {
                throw new PassInException("The title  is invalid.");
            }

            if (string.IsNullOrWhiteSpace(request.Details))
            {
                throw new PassInException("The detail is invalid.");
            }
        }

    }
}
