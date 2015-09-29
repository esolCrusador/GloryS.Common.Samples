using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GloryS.LinqSample.DAL.DataEntities;

namespace GloryS.LinqSample.Models
{
    public class EnrollmentModel : IPropertiesMapper<Enrollment, EnrollmentModel>
    {
        public int EnrollmentId { get; set; }
        public Grade? Grade { get; set; }

        public void MapProperties(Enrollment source, EnrollmentModel dest)
        {
            dest.EnrollmentId = source.EnrollmentID;
            dest.Grade = source.Grade;
        }

        public override string ToString()
        {
            return "EnrollmentId: " + EnrollmentId + " " + "Grade: " + Grade.ToString();
        }
    }
}

