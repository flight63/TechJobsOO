using Microsoft.AspNetCore.Mvc;
using TechJobs.Data;
using TechJobs.ViewModels;
using TechJobs.Models;

namespace TechJobs.Controllers
{
    public class JobController : Controller
    {

        // Our reference to the data store
        private static JobData jobData;

        static JobController()
        {
            jobData = JobData.GetInstance();
        }
        //TODO#1
        // The detail display for a given Job at URLs like /Job?id=17
        public IActionResult Index(int id)
        {
            Job singleJob = JobData.GetInstance().Find(id);
            //Job singleJob = jobData.Find(id);
            return View(singleJob);
        }

        public IActionResult New()
        {
            NewJobViewModel newJobViewModel = new NewJobViewModel();
            return View(newJobViewModel);
        }

        [HttpPost]
        public IActionResult New(NewJobViewModel newJobViewModel)
        {
            // TODO #6 - Validate the ViewModel and if valid, create a 
            // new Job and add it to the JobData data store. Then
            // redirect to the Job detail (Index) action/view for the new Job.

            if (!ModelState.IsValid)
            {
                return View(newJobViewModel);
            }

            else
            {
                JobData data = JobData.GetInstance();
                Job newJob = new Job();

                newJob.Name = newJobViewModel.Name;
                newJob.Employer = data.Employers.Find(newJobViewModel.EmployerID);
                newJob.Location = data.Locations.Find(newJobViewModel.LocationID);
                newJob.CoreCompetency = data.CoreCompetencies.Find(newJobViewModel.CoreCompetencyID);
                newJob.PositionType = data.PositionTypes.Find(newJobViewModel.PositionTypeID);


                jobData.Jobs.Add(newJob);

                return Redirect(string.Format("/Job?id={0}", newJob.ID));
            };


        }
    }
}
