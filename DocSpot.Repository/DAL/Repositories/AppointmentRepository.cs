using DocSpot.Models;
using DocSpot.Repository.DAL.Interfaces;
using DocSpot.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocSpot.Repository.DAL.Repositories
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly DocSpotDBContext _dbContext;
        public AppointmentRepository(DocSpotDBContext dBContext)
        {
            _dbContext = dBContext;
        }
        public async Task<List<AppointmentVM>> GetAppointmentsByPatientId(int patientId)
        {
            try
            {
                var query =  _dbContext.Appointments.Where(x => x.PatientId == patientId);
                return await query.Select(x => new AppointmentVM { 
                    Id = x.Id,
                    DoctorId = x.DoctorId,
                    DoctorName = x.Doctor.FirstName + " " + x.Doctor.LastName,
                    DepartmentName = x.Doctor.Department.Name ?? "General Practicial",
                    HospitalName = x.Doctor.Hospital.Name,
                    DepartmentId = x.Doctor.DepartmentId,
                    HospitalId  = x.Doctor.HospitalId,
                    AppointmentDate = x.AppointmentDate,
                    AppointmentTime = x.AppointmentTime,
                    IsApproved = x.IsApproved,

                }).ToListAsync();

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<AppointmentVM>> GetAppointmentsByDoctorId(int doctorId)
        {
            try
            {
                var query = _dbContext.Appointments.Where(x => x.DoctorId == doctorId);
                return await query.Select(x => new AppointmentVM
                {
                    Id = x.Id,
                    PatientId = x.PatientId,
                    PatientName = x.Patient.FirstName + " " + x.Patient.LastName,
                    DepartmentName = x.Doctor.Department.Name ?? "General Practicial",
                    HospitalName = x.Doctor.Hospital.Name,
                    DepartmentId = x.Doctor.DepartmentId,
                    HospitalId = x.Doctor.HospitalId,
                    AppointmentDate = x.AppointmentDate,
                    AppointmentTime = x.AppointmentTime,
                    IsApproved = x.IsApproved,

                }).ToListAsync();

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task ApproveAppointment(int appointmentId)
        {
            try
            {
                var doctor = await _dbContext.Appointments.FirstOrDefaultAsync(x => x.Id == appointmentId);
                doctor.IsApproved = !doctor.IsApproved;
                await _dbContext.SaveChangesAsync();    
            }
            catch (Exception)
            {

                throw;
            }        }

        public async Task<AppointmentVM> GetById(int Id)
        {
            try
            {
                var query = _dbContext.Appointments.Where(x => x.Id == Id);
                return await query.Select(x => new AppointmentVM
                {
                    Id = x.Id,
                    DoctorId = x.DoctorId,
                    DoctorName = x.Doctor.FirstName + " " + x.Doctor.LastName,
                    DepartmentName = x.Doctor.Department.Name ?? "General Practician",
                    HospitalName = x.Doctor.Hospital.Name,
                    DepartmentId = x.Doctor.DepartmentId,
                    HospitalId = x.Doctor.HospitalId,
                    AppointmentDate = x.AppointmentDate,
                    AppointmentTime = x.AppointmentTime,
                    IsApproved = x.IsApproved,

                }).FirstOrDefaultAsync();

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task Add(AppointmentVM appointment)
        {
            try
            {
                var add = new Appointment()
                {
                    DoctorId = appointment.DoctorId,
                    PatientId = appointment.PatientId,
                    AppointmentDate = appointment.AppointmentDate,
                    AppointmentTime = appointment.AppointmentTime,
                    IsApproved = false,
                };
                await _dbContext.AddAsync(add);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task Update(AppointmentVM appointment)
        {
            try
            {
                var model = await _dbContext.Appointments.FirstOrDefaultAsync(x => x.Id == appointment.Id);
                model.DoctorId = appointment.DoctorId;
                //model.PatientId = appointment.PatientId;
                model.AppointmentDate = appointment.AppointmentDate;
                model.AppointmentTime = appointment.AppointmentTime;
                model.IsApproved = appointment.IsApproved ?? false;
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
