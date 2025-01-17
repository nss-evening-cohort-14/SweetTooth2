﻿using Microsoft.AspNetCore.Mvc;
using SweetTooth.DataAccess;
using SweetTooth.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SweetTooth.Controllers
{
    [Route("api/snackMood")]
    [ApiController]
    public class SnackMoodController : ControllerBase
    {
        SnackMoodRepo _snackMoodRepo;

        public SnackMoodController(SnackMoodRepo snackMoodRepo)
        {
            _snackMoodRepo = snackMoodRepo;
        }

        [HttpGet]
        public IActionResult GetAllSnackMoods()
        {
            return Ok(_snackMoodRepo.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult GetSnackMoodById(Guid id)
        {
            SnackMood snackMood = _snackMoodRepo.GetById(id);

            if (snackMood == null)
            {
                return NotFound("No snackMood exists with that id");
            }

            return Ok(snackMood);
        }

        [HttpPost]
        public IActionResult AddSnackMood(SnackMood newSnackMood)
        {
            var snackMoodExists = _snackMoodRepo.GetBySnackIdAndMoodId(newSnackMood.MoodId, newSnackMood.SnackId);
            if (snackMoodExists != null)
                return BadRequest("SnackMood combination already exists.");

            if (newSnackMood.MoodId == Guid.Empty || newSnackMood.SnackId == Guid.Empty)
                return BadRequest("Snack and/or Mood Id's are required.");

            _snackMoodRepo.Add(newSnackMood);

            return Created($"/api/snackMood/{newSnackMood.Id}", newSnackMood);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteSnackMood(Guid id)
        {
            _snackMoodRepo.Remove(id);
            
            return Ok();
        }

    }
}
