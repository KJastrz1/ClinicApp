// using ClinicApp.Application.Abstractions.Messaging;
// using ClinicApp.Application.ReadRepositories;
// using ClinicApp.Domain.ApiContracts;
// using ApiContracts.Contracts.Doctor.Responses;
// using System;
// using System.Collections.Generic;
// using System.Threading;
// using System.Threading.Tasks;
//
// namespace ClinicApp.Application.UseCases.Doctors.Query.GetAvailableAppointments
// {
//     internal sealed class GetAvailableAppointmentsQueryHandler : IQueryHandler<GetAvailableAppointmentsQuery, List<AvailableAppointmentsResponse>>
//     {
//         private readonly IDoctorReadRepository _doctorReadRepository;
//
//         public GetAvailableAppointmentsQueryHandler(IDoctorReadRepository doctorReadRepository)
//         {
//             _doctorReadRepository = doctorReadRepository;
//         }
//
//         public async Task<Result<List<AvailableAppointmentsResponse>>> Handle(GetAvailableAppointmentsQuery request, CancellationToken cancellationToken)
//         {
//             // Pobierz dostępne terminy dla danego lekarza z repozytorium, w określonym zakresie dat
//             var availableAppointments = await _doctorReadRepository.GetAvailableAppointmentsAsync(
//                 doctorId: request.DoctorId,
//                 startDate: request.StartDate,
//                 endDate: request.EndDate,
//                 cancellationToken: cancellationToken
//             );
//
//             // Przekształć dane w odpowiedź API
//             var response = availableAppointments.Select(appointment => new AvailableAppointmentsResponse(
//                 Clinic: appointment.Clinic.MapToResponse(), // Zakładam, że masz metodę mapowania ClinicReadModel na ClinicResponse
//                 Dates: appointment.Dates // Lista DateTime dostępnych terminów
//             )).ToList();
//
//             return Result.Success(response);
//         }
//     }
// }
