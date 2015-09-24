using System;
using System.Linq.Expressions;
using GloryS.LinqSample.DAL.DataEntities;

namespace GloryS.LinqSample.Models
{
    public class EnrollmentBaseModel: ISelectExpression<Enrollment, EnrollmentBaseModel>
    {
        public int EnrollmentId { get; set; }
        public Grade? Grade { get; set; }
        public string GradeString { get; set; }

        Expression<Func<Enrollment, EnrollmentBaseModel>> ISelectExpression<Enrollment, EnrollmentBaseModel>.GetSelectExpression()
        {
            Expression<Func<Enrollment, EnrollmentBaseModel>> select = enrollment => new EnrollmentBaseModel
            {
                EnrollmentId = enrollment.EnrollmentID,
                Grade = enrollment.Grade,
            };

            select = select.AddMemberInit(e => e.Grade, r => r.GradeString, EnumExpressionExtensions.GetEnumToStringExpression<Grade?>(g => g.ToString()));

            return select;
        }

        public override string ToString()
        {
            return String.Format("Enrollment ({0}) ({1})", EnrollmentId, Grade.ToString());
        }
    }
}
