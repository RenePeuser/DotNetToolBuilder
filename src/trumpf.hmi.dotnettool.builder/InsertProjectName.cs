using System.Drawing;
using Pastel;

namespace trumpf.hmi.dotnettool.builder
{
    public class InsertProjectName : CollectInfoStep
    {
        public InsertProjectName() : base(
            "Please enter the name of your project: (Sample: Trumpf.Hmi.New.Submarine)".AsInput())
        {
        }
    }
}