using AutoMapper;
using Microsoft.AspNet.Identity;
using Notes.Model;
using Notes.Service;
using Notes.Web.Infrastructure.Models;
using Notes.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Notes.Web.Controllers
{
    [Authorize]
    public class NoteController : Controller
    {
        #region Fields
        private readonly INoteService _noteService;
        #endregion

        #region Constructors
        public NoteController(INoteService noteService)
        {
            _noteService = noteService;
        }
        #endregion

        #region Actions
        public async Task<ActionResult> Index(string noteName, string noteText, DateTime? date, int page = 1, int notesPerPage = 12)
        {
            int notesFound = 0;
            IEnumerable<Note> notes = await Task<IEnumerable<Note>>.Run(() => 
            {
                return _noteService.GetFilteredNotes(noteName, noteText, date, page, notesPerPage, out notesFound);
            });
            IEnumerable<NoteViewModel> noteViewModels = Mapper.Map<IEnumerable<Note>, IEnumerable<NoteViewModel>>(notes);
            NoteListViewModel noteListViewModel = new NoteListViewModel
            {
                NoteViewModels = noteViewModels,
                PagingInfo = new PagingInfo 
                {
                    CurrentPage = page,
                    ItemsPerPage = notesPerPage,
                    TotalItems = notesFound
                }
            };

            return View(noteListViewModel);
        }

        public ActionResult Details(int? id) 
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Note note = _noteService.GetNoteById(id.Value);

            if (note == null)
                return HttpNotFound();

            NoteViewModel noteViewModel = Mapper.Map<Note, NoteViewModel>(note);

            return View(noteViewModel);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(NoteViewModel noteViewModel)
        {
            if (ModelState.IsValid)
            {
                Note note = Mapper.Map<NoteViewModel, Note>(noteViewModel);
                _noteService.AddNote(note);
                await _noteService.CommitAsync();

                return RedirectToAction("Index");
            }

            return View(noteViewModel);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Note note = _noteService.GetNoteById(id.Value);

            if (note == null)
                return HttpNotFound();

            NoteViewModel noteViewModel = Mapper.Map<Note, NoteViewModel>(note);

            return View(noteViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]   
        public async Task<ActionResult> Edit(NoteViewModel noteViewModel)
        {
            if (ModelState.IsValid)
            {
                Note note = Mapper.Map<NoteViewModel, Note>(noteViewModel);
                _noteService.UpdateNote(note);
                await _noteService.CommitAsync();

                return RedirectToAction("Index");
            }

            return View(noteViewModel);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Note note = _noteService.GetNoteById(id.Value);

            if(note == null)
                return HttpNotFound();

            NoteViewModel noteViweModel = Mapper.Map<Note, NoteViewModel>(note);

            return View(noteViweModel);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            _noteService.RemoveNoteById(id.Value);
            await _noteService.CommitAsync();

            return RedirectToAction("Index");
        }
        #endregion

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            _noteService.UserId = HttpContext.User.Identity.GetUserId();
            base.OnActionExecuting(filterContext);
        }
    }
}
