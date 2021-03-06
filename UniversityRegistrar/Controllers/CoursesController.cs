using Microsoft.AspNetCore.Mvc;
using UniversityRegistrar.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace UniversityRegistrar.Controllers
{
  public class CoursesController : Controller
  {
    private readonly UniversityRegistrarContext _db;
    
    public CoursesController(UniversityRegistrarContext db)
    {
      _db = db; 
    }
    public ActionResult Index()
    {
      List<Course> model = _db.Courses.ToList();
      return View(model);
    }
    public ActionResult Create()
    {
      return View();
    }
    [HttpPost]
    public ActionResult Create(Course course)
    {
      _db.Courses.Add(course);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      Course selectedCourse = _db.Courses
        .Include(course => course.Students)
        .ThenInclude(join => join.Student)
        .FirstOrDefault(course => course.CourseId == id);
      return View(selectedCourse);
    }

    public ActionResult Edit(int id)
    {
      Course selectedCourse = _db.Courses.FirstOrDefault(course => course.CourseId == id);
      return View(selectedCourse);
    }

    [HttpPost]
    public ActionResult Edit(Course course)
    {
      _db.Entry(course).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      Course selectedCourse = _db.Courses.FirstOrDefault(course => course.CourseId == id);
      return View(selectedCourse);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      Course selectedCourse = _db.Courses.FirstOrDefault(course => course.CourseId ==id);
      _db.Courses.Remove(selectedCourse);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}