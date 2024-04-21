using DocSpot.Models;
using DocSpot.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocSpot.Repository.DAL.Interfaces
{
    public interface IAppointmentRepository
    {
        Task<List<AppointmentVM>> GetAppointmentsByPatientId(int patientId);
        Task<List<AppointmentVM>> GetAppointmentsByDoctorId(int doctorId);
        Task Add(AppointmentVM appointment);
        Task Update(AppointmentVM appointment);
        Task<AppointmentVM> GetById(int Id);
        Task ApproveAppointment(int appointmentId);
    }
}
