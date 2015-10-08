using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using GloryS.LinqSample.DAL;
using GloryS.LinqSample.DAL.DataEntities;
using LinqExpressionsMapper;

namespace GloryS.LinqSample.AllExamples
{
    public static class CultureResolveExample
    {
        public static void ShowCourses(SchoolContext context)
        {
            Culture[] supportedCultrues = new[]
            {
                Culture.Default,
                Culture.DE,
                Culture.ES,
                Culture.RU
            };

            //Localized course names.
            var courses = context.Courses.ResolveSelectExternal<Course, CourseModel, Culture>(Culture.DE).ToList();

            //Students with localized course names.
            var studentCourses = context.Students.ResolveSelect<Student, StudentWithCourses, Culture>(Culture.ES).ToList();
        }

        public class CourseModel: ISelectExpression<Course, CourseModel, Culture>
        {
            public int CourseId { get; set; }

            public string CourseName { get; set; }
            public Expression<Func<Course, CourseModel>> GetSelectExpression(Culture cultureId)
            {
                return course => new CourseModel
                {
                    CourseId = course.CourseID,
                    CourseName = (course.CourseRes.FirstOrDefault(c => c.Culture == cultureId)
                                  ?? course.CourseRes.FirstOrDefault(c => c.Culture == Culture.Default))
                        .Title
                };
            }
        }

        public class StudentWithCourses: ISelectExpression<Student, StudentWithCourses, Culture>
        {
            public int StudentId { get; set; }

            public string StudentFullName { get; set; }

            public IEnumerable<CourseModel> Courses { get; set; }
            public Expression<Func<Student, StudentWithCourses>> GetSelectExpression(Culture cultureId)
            {
                Expression<Func<Student, StudentWithCourses>> select = student => new StudentWithCourses
                {
                    StudentId = student.ID,
                    StudentFullName = student.FirstMidName + " " + student.LastName
                };

                select = select.AddMemberInit(student => student.Enrollments.Select(enr => enr.Course), studentModel => studentModel.Courses, Mapper.GetExternalExpression<Course, CourseModel, Culture>(cultureId));

                return select;
            }
        }
    }
}
