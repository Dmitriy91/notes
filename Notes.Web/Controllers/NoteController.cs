using AutoMapper;
using Notes.Entities;
using Notes.Services;
using Notes.Web.Infrastructure.Models;
using Notes.Web.ViewModels;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Notes.Web.Controllers
{
    [Authorize]
    public class NoteController : Controller
    {
        #region Fields
        private INoteService _noteService;
        #endregion

        #region Constructors
        public NoteController(INoteService noteService)
        {
            _noteService = noteService;
        }
        #endregion

        #region Actions
        public async Task<ActionResult> Index(NoteFilter noteFilter, int page = 1, int notesPerPage = 12)
        {
            noteFilter = noteFilter ?? new NoteFilter();
            int notesFound = 0;
            IEnumerable<Note> notes = null;

            if (ModelState.IsValid)
            {
                notes = await Task.Run(() =>
                {
                    return _noteService.GetFilteredNotes(noteFilter.Name, noteFilter.Text, noteFilter.Date, page, notesPerPage, out notesFound);
                });
            }

            IEnumerable<NoteViewModel> noteViewModels = Mapper.Map<IEnumerable<Note>, IEnumerable<NoteViewModel>>(notes);
            NoteListViewModel noteListViewModel = new NoteListViewModel
            {
                NoteViewModels = noteViewModels,
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = notesPerPage,
                    TotalItems = notesFound
                },
                NoteFilter = noteFilter
            };

            return View(noteListViewModel);
        }

        public async Task<ActionResult> Details(int? id) 
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Note note = await Task.Run(() => _noteService.GetNoteById(id.Value));

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
                TempData["StatusBarInfo"] = new StatusBarInfo
                {
                    Message = "Successfully created.",
                    Type = StatusBarInfo.StatusBarType.Success
                };

                return RedirectToAction("Index");
            }

            return View(noteViewModel);
        }

        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Note note = await Task.Run(() => _noteService.GetNoteById(id.Value));

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

                if (_noteService.UpdateNote(note))
                {
                    await _noteService.CommitAsync();
                    TempData["StatusBarInfo"] = new StatusBarInfo
                    {
                        Message = "Successfully updated.",
                        Type = StatusBarInfo.StatusBarType.Success
                    };
                }
                else 
                {
                    TempData["StatusBarInfo"] = new StatusBarInfo
                    {
                        Message = "Unauthorized actions detected.",
                        Type = StatusBarInfo.StatusBarType.Warning
                    };
                }

                return RedirectToAction("Index");
            }

            return View(noteViewModel);
        }

        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Note note = await Task.Run(() => _noteService.GetNoteById(id.Value));

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

            if (_noteService.RemoveNoteById(id.Value))
            {
                await _noteService.CommitAsync();
                TempData["StatusBarInfo"] = new StatusBarInfo
                {
                    Message = "Successfully deleted.",
                    Type = StatusBarInfo.StatusBarType.Success
                };
            }
            else 
            {
                TempData["StatusBarInfo"] = new StatusBarInfo
                {
                    Message = "Unauthorized actions detected.",
                    Type = StatusBarInfo.StatusBarType.Warning
                };
            }

            return RedirectToAction("Index");
        }
        #endregion

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_noteService != null)
                {
                    _noteService.Dispose();
                    _noteService = null;
                }
            }

            base.Dispose(disposing);
        }
    }
}
