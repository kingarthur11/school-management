﻿using Shared.Constants;
using Shared.Enums;
using Shared.Models.Responses;
using static Shared.Constants.StringConstants;
using MediatR;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Models.Responses;
using Models.Requests;
using Core.Entities.Users;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Identity;
using Infrastructure.Data;
using Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using Shared.Models.Requests;
using Core.Entities;

namespace Core.Services
{
    public class PersonaService : IPersonaService
    {
        private readonly UserManager<User> _userManager;
        private readonly ILogger<PersonaService> _logger;
        private readonly HttpContext _httpContext;
        private readonly AppDbContext _dbContext;
        private readonly IWebHostEnvironment _webHost;
        private readonly IMediator _mediator;

        public PersonaService(
            UserManager<User> userManager,
            ILogger<PersonaService> logger,
            IHttpContextAccessor httpContextAccessor,
            AppDbContext dbContext,
            IWebHostEnvironment webHost,
            IMediator mediator)
        {
            _userManager = userManager;
            _logger = logger;
            _httpContext = httpContextAccessor.HttpContext!;
            _dbContext = dbContext;
            _webHost = webHost;
            _mediator = mediator;
        }


        private static string GetRoleToAdd(PersonaType type)
        {
            var role = type switch
            {
                PersonaType.Parent => AuthConstants.Roles.PARENT,
                PersonaType.Student => AuthConstants.Roles.STUDENT,
                PersonaType.Staff => AuthConstants.Roles.STAFF,
                PersonaType.BusDriver => AuthConstants.Roles.BUS_DRIVER,
                _ => default,
            };
            return role;
        }

        public async Task<ApiResponse<ParentResponse>> CreateParentAsync(CreateParentRequest request, string host, string tenantId, string email)
        {
            var response = new ApiResponse<ParentResponse>() { Code = ResponseCodes.Status201Created };
            if (!string.Equals(request.Password, request.ConfirmPassword))
            {
                response.Status = false;
                response.Code = ResponseCodes.Status400BadRequest;
                response.Message = "Password Field not the same as that of ConfirmPassword field";
                return response;
            }

            _logger.LogInformation("Creating a new Parent");
            _logger.LogInformation("Trying to upload photo of new Parent");

            string photoUrl = UploadPhoto(request.Photo, "parent-images", "parent-");
            if (string.IsNullOrWhiteSpace(photoUrl))
            {
                response.Status = false;
                response.Message = "Unable to upload parent's photo. Please try again.";
                response.Code = ResponseCodes.Status500InternalServerError;
                return response;
            }
            _logger.LogInformation("Photo uploaded successfully");

            var user = new User()
            {
                UserName = request.Email,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                FirstName = request.FirstName,
                LastName = request.LastName,
                EmailConfirmed = true,
                TenantId = tenantId,
                PhotoUrl = photoUrl,
                PesonaType = PersonaType.Parent
            };
            var creationResult = await _userManager.CreateAsync(user, request.Password);
            if (!creationResult.Succeeded)
            {
                response.Code = ResponseCodes.Status400BadRequest;
                response.Status = false;
                response.Message = string.Join(',', creationResult.Errors.Select(a => a.Description));
                _logger.LogInformation("Parent Creation is not successful with the following error {0}", response.Message);
                return response;
            }

            var parent = new Parent()
            {
                Id = Guid.NewGuid(),
                FirstName = request.FirstName,
                LastName = request.LastName,
                PhotoUrl = photoUrl,
                PersonaId = user.Id,
                TenantId = tenantId,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
            };
            await _dbContext.Parents.AddAsync(parent);
            if (!await _dbContext.TrySaveChangesAsync())
            {
                _logger.LogInformation("Unable to create Parent entity.");
                response.Status = false;
                response.Message = "Unable to create Parent entity.";
                response.Code = ResponseCodes.Status500InternalServerError;
                return response;
            }
            _logger.LogInformation("Parent Creation is successful");

            var personaResponse = new PersonaResponse() { Email = user.Email, FirstName = user.FirstName, LastName = user.LastName, PhoneNumber = user.PhoneNumber };
            response.Data = new ParentResponse() { PhotoUrl = user.PhotoUrl, Email = user.Email, FirstName = user.FirstName, ParentId = parent.Id, LastName = user.LastName, PhoneNumber = user.PhoneNumber, UserName = user.UserName, Role = AuthConstants.Roles.PARENT };
            return response;
        }

        public async Task<ApiResponse<StudentResponse>> CreateStudentAsync(CreateStudentRequest request, string host, string tenantId, string email)
        {
            var response = new ApiResponse<StudentResponse>() { Code = ResponseCodes.Status201Created };

            if (!string.Equals(request.Password, request.ConfirmPassword))
            {
                response.Status = false;
                response.Code = ResponseCodes.Status400BadRequest;
                response.Message = "Password Field not the same as that of ConfirmPassword field";
                return response;
            }

            var parent = await _dbContext.Parents.FirstOrDefaultAsync(x => x.Id == request.ParentId);
            if (parent is null)
            {
                response.Code = ResponseCodes.Status400BadRequest;
                response.Status = false;
                response.Message = "Invalid ParentId";
                return response;
            }

            _logger.LogInformation("Creating a new Student");
            _logger.LogInformation("Trying to upload photo of new Student");

            string photoUrl = UploadPhoto(request.Photo, "student-images", "student-");
            if (string.IsNullOrWhiteSpace(photoUrl))
            {
                response.Status = false;
                response.Message = "Unable to upload student's photo. Please try again.";
                response.Code = ResponseCodes.Status500InternalServerError;
                return response;
            }
            _logger.LogInformation("Photo uploaded successfully");

            User user;
            string emailExist = string.Concat(request.FirstName, request.LastName, "@", "smsabuja", ".com");

            int sameEmailCount = await _dbContext.Users.CountAsync(x => x.Email == emailExist);
            if (sameEmailCount != 0)
            {
                emailExist = string.Concat(request.FirstName, request.LastName, $"{sameEmailCount++}", "@", "smsabuja", ".com");
                user = new User()
                {
                    UserName = string.Concat(request.FirstName, request.LastName),
                    Email = emailExist,
                    PhoneNumber = parent.PhoneNumber,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    EmailConfirmed = true,
                    TenantId = tenantId,
                    PhotoUrl = photoUrl,
                    PesonaType = PersonaType.Student
                };
            }
            else
            {
                user = new User()
                {
                    UserName = string.Concat(request.FirstName, request.LastName),
                    Email = string.Concat(request.FirstName, request.LastName, "@", "smsabuja", ".com"),
                    PhoneNumber = parent.PhoneNumber,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    EmailConfirmed = true,
                    TenantId = tenantId,
                    PhotoUrl = photoUrl,
                    PesonaType = PersonaType.Student
                };
            }

            var creationResult = await _userManager.CreateAsync(user, request.Password);
            if (!creationResult.Succeeded)
            {
                response.Code = ResponseCodes.Status400BadRequest;
                response.Status = false;
                response.Message = string.Join(',', creationResult.Errors.Select(a => a.Description));
                _logger.LogInformation("Student Creation is not successful with the following error {0}", response.Message);
                return response;
            }

            Student student;

            if (request.BusServiceRequired)
            {
                var bus = await _dbContext.Buses.FirstOrDefaultAsync(x => x.Id == request.BusId);
                if (bus is null)
                {
                    response.Status = false;
                    response.Code = ResponseCodes.Status400BadRequest;
                    response.Message = "Bus doesn't exist";
                    return response;
                }

                student = new Student()
                {
                    Id = Guid.NewGuid(),
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    PhotoUrl = photoUrl,
                    PersonaId = user.Id,
                    TenantId = tenantId,
                    Email = user.Email,
                    GradeId = request.GradeId,
                    BusId = request.BusId,
                    BusServiceRequired = request.BusServiceRequired,
                };
            }
            else
            {
                student = new Student()
                {
                    Id = Guid.NewGuid(),
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    PhotoUrl = photoUrl,
                    PersonaId = user.Id,
                    TenantId = tenantId,
                    Email = user.Email,
                    GradeId = request.GradeId,
                    BusServiceRequired = request.BusServiceRequired,
                };
            }

            await _dbContext.Students.AddAsync(student);
            if (!await _dbContext.TrySaveChangesAsync())
            {
                _logger.LogInformation("Unable to create Student entity.");
                response.Status = false;
                response.Message = "Unable to create Student entity.";
                response.Code = ResponseCodes.Status500InternalServerError;
                return response;
            }

            var parentStudent = new ParentStudent()
            {
                StudentsId = student.Id,
                ParentsId = parent.Id,
            };
            await _dbContext.ParentStudent.AddAsync(parentStudent);
            await _dbContext.TrySaveChangesAsync();

            _logger.LogInformation("Student Creation is successful");

            var personaResponse = new PersonaResponse() { Email = user.Email, FirstName = user.FirstName, LastName = user.LastName, PhoneNumber = user.PhoneNumber };

            response.Data = new StudentResponse() { StudentId = student.Id, PhotoUrl = user.PhotoUrl, FirstName = user.FirstName, BusServiceRequired = request.BusServiceRequired, LastName = user.LastName, Role = AuthConstants.Roles.STUDENT };

            return response;
        }

        public async Task<BaseResponse> CreateBusDriverAsync(CreateBusDriverRequest request, string host, string tenantId, string email)
        {
            var response = new BaseResponse() { Code = ResponseCodes.Status201Created };

            bool isBusSelected = await _dbContext.Busdrivers.AnyAsync(x => x.BusId == request.BusId);
            if (isBusSelected)
            {
                response.Status = false;
                response.Code = ResponseCodes.Status400BadRequest;
                response.Message = "Please select another bus. This bus number has a driver attached to it";
                return response;
            }

            var bus = await _dbContext.Buses.FirstOrDefaultAsync(x => x.Id == request.BusId);
            if (bus is null)
            {
                response.Status = false;
                response.Code = ResponseCodes.Status400BadRequest;
                response.Message = "Bus doesn't exist";
                return response;
            }

            if (!string.Equals(request.Password, request.ConfirmPassword))
            {
                _logger.LogInformation("Password Field not the same as that of ConfirmPassword field");
                response.Status = false;
                response.Code = ResponseCodes.Status400BadRequest;
                response.Message = "Password Field not the same as that of ConfirmPassword field";
                return response;
            }

            _logger.LogInformation("Creating a new user");
            _logger.LogInformation("Trying to upload photo of new Bus driver");

            string photoUrl = UploadPhoto(request.Photo, "busdriver-images", "busdriver-");
            if (string.IsNullOrWhiteSpace(photoUrl))
            {
                _logger.LogInformation("Unable to upload bus driver photo. Please try again.");
                response.Status = false;
                response.Message = "Unable to upload bus driver photo. Please try again.";
                response.Code = ResponseCodes.Status500InternalServerError;
                return response;
            }
            _logger.LogInformation("Photo uploaded successfully");


            var user = new User()
            {
                UserName = request.Email,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                FirstName = request.FirstName,
                LastName = request.LastName,
                EmailConfirmed = true,
                TenantId = tenantId,
                PhotoUrl = photoUrl,
                PesonaType = PersonaType.BusDriver
            };

            var creationResult = await _userManager.CreateAsync(user, request.Password);
            if (!creationResult.Succeeded)
            {
                response.Code = ResponseCodes.Status400BadRequest;
                response.Status = false;
                response.Message = string.Join(',', creationResult.Errors.Select(a => a.Description));
                _logger.LogInformation("Bus driver Creation is not successful with the following error {0}", response.Message);
                return response;
            }

            var busDriver = new Busdriver()
            {
                Id = Guid.NewGuid(),
                FirstName = request.FirstName,
                LastName = request.LastName,
                PhotoUrl = photoUrl,
                PersonaId = user.Id,
                TenantId = tenantId,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                BusId = request.BusId,
            };
            await _dbContext.Busdrivers.AddAsync(busDriver);
            if (!await _dbContext.TrySaveChangesAsync())
            {
                _logger.LogInformation("Unable to create Bus driver entity.");
                response.Status = false;
                response.Message = "Unable to create Bus Driver entity.";
                response.Code = ResponseCodes.Status500InternalServerError;
                return response;
            }
            _logger.LogInformation("Bus driver Creation is successful");

            var personaResponse = new PersonaResponse() { Email = user.Email, FirstName = user.FirstName, LastName = user.LastName, PhoneNumber = user.PhoneNumber };

            return response;
        }

        public async Task<BaseResponse> CreateStaffAsync(CreateStaffRequest request, string host, string tenantId, string email)
        {
            var response = new BaseResponse() { Code = ResponseCodes.Status201Created };
            if (!string.Equals(request.Password, request.ConfirmPassword))
            {
                _logger.LogInformation("Password Field not the same as that of ConfirmPassword field");
                response.Status = false;
                response.Code = ResponseCodes.Status400BadRequest;
                response.Message = "Password Field not the same as that of ConfirmPassword field";
                return response;
            }

            _logger.LogInformation("Creating a new staff");
            _logger.LogInformation("Trying to upload photo of new Staff");

            string photoUrl = UploadPhoto(request.Photo, "staff-images", "staff-");
            if (string.IsNullOrWhiteSpace(photoUrl))
            {
                _logger.LogInformation("Unable to upload staff photo. Please try again.");
                response.Status = false;
                response.Message = "Unable to upload staff photo. Please try again.";
                response.Code = ResponseCodes.Status500InternalServerError;
                return response;
            }
            _logger.LogInformation("Photo uploaded successfully");


            var user = new User()
            {
                UserName = request.Email,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                FirstName = request.FirstName,
                LastName = request.LastName,
                EmailConfirmed = true,
                TenantId = tenantId,
                PhotoUrl = photoUrl,
                PesonaType = PersonaType.Staff
            };

            var creationResult = await _userManager.CreateAsync(user, request.Password);
            if (!creationResult.Succeeded)
            {
                response.Code = ResponseCodes.Status400BadRequest;
                response.Status = false;
                response.Message = string.Join(',', creationResult.Errors.Select(a => a.Description));
                _logger.LogInformation("Staff Creation is not successful with the following error {0}", response.Message);
                return response;
            }

            var staff = new Staff()
            {
                Id = Guid.NewGuid(),
                FirstName = request.FirstName,
                LastName = request.LastName,
                PhotoUrl = photoUrl,
                PersonaId = user.Id,
                TenantId = tenantId,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
            };
            await _dbContext.Staffs.AddAsync(staff);
            if (!await _dbContext.TrySaveChangesAsync())
            {
                _logger.LogInformation("Unable to create Staff entity.");
                response.Status = false;
                response.Message = "Unable to create Staff entity.";
                response.Code = ResponseCodes.Status500InternalServerError;
                return response;
            }
            _logger.LogInformation("Staff Creation is successful");

            var personaResponse = new PersonaResponse() { Email = user.Email, FirstName = user.FirstName, LastName = user.LastName, PhoneNumber = user.PhoneNumber };

            return response;
        }

        public async Task<ApiResponse<List<ParentResponse>>> ParentListAsync()
        {
            var response = new ApiResponse<List<ParentResponse>>();

            var parents = await _dbContext.Parents
                .Select(x => new ParentResponse()
                {
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    ParentId = x.Id,
                    Email = x.Email,
                    PhoneNumber = x.PhoneNumber,
                    PhotoUrl = x.PhotoUrl,
                })
                .AsNoTracking()
                .ToListAsync();

            foreach (var parent in parents)
            {
                parent.NumberOfStudent = _dbContext.ParentStudent.Where(x => x.ParentsId == parent.ParentId).Count();
            }

            response.Data = parents;
            return response;
        }

        public async Task<ApiResponse<List<StudentResponse>>> StudentListAsync()
        {
            var response = new ApiResponse<List<StudentResponse>>();

            var students = _dbContext.Students
                .Select(x => new StudentResponse()
                {
                    StudentId = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    PhotoUrl = x.PhotoUrl,
                })
                .AsNoTracking();

            response.Data = await students.ToListAsync();
            return response;
        }

        public async Task<ApiResponse<List<StudentResponse>>> ParentStudentsListAsync(Guid parentId)
        {
            var response = new ApiResponse<List<StudentResponse>>();

            var studentIds = await _dbContext.ParentStudent
                .Where(x => x.ParentsId == parentId)
                .Select(x => x.StudentsId)
                .ToListAsync();

            var students = _dbContext.Students
                .Where(x => studentIds.Contains(x.Id))
                .Select(x => new StudentResponse()
                {
                    StudentId = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    PhotoUrl = x.PhotoUrl,
                })
                .AsNoTracking();

            response.Data = await students.ToListAsync();
            return response;
        }


        public async Task<BaseResponse> EditParentAsync(Guid parentId, EditParentRequest request, string editor)
        {
            var response = new BaseResponse();

            var parent = await _dbContext.Parents.FirstOrDefaultAsync(x => x.Id == parentId);
            if (parent is null)
            {
                response.Code = ResponseCodes.Status404NotFound;
                response.Status = false;
                response.Message = "Parent not found";
                return response;
            }

            parent.FirstName = request.FirstName ?? parent.FirstName;
            parent.LastName = request.LastName ?? parent.LastName;
            parent.Edit(editor);

            if (!(await _dbContext.TrySaveChangesAsync()))
            {
                response.Code = ResponseCodes.Status500InternalServerError;
                response.Status = false;
                response.Message = "Unable to update parent";
                return response;
            }

            var persona = await _userManager.FindByIdAsync(parent.PersonaId.ToString());
            if (persona is null)
            {
                _logger.LogInformation("Parent account not found. Update not completed");
            }
            else
            {
                persona.FirstName = request.FirstName ?? persona.FirstName;
                persona.LastName = request.LastName ?? persona.LastName;
                await _userManager.UpdateAsync(persona);
            }

            return response;
        }


        public async Task<ApiResponse<StudentResponse>> EditStudentAsync(Guid studentId, EditStudentRequest request, string editor)
        {
            var response = new ApiResponse<StudentResponse>();

            var student = await _dbContext.Students.FirstOrDefaultAsync(x => x.Id == studentId);
            if (student is null)
            {
                response.Code = ResponseCodes.Status404NotFound;
                response.Status = false;
                response.Message = "Student not found";
                return response;
            }

            student.FirstName = request.FirstName ?? student.FirstName;
            student.LastName = request.LastName ?? student.LastName;
            student.Edit(editor);

            if (!(await _dbContext.TrySaveChangesAsync()))
            {
                response.Code = ResponseCodes.Status500InternalServerError;
                response.Status = false;
                response.Message = "Unable to update student";
                return response;
            }

            var persona = await _userManager.FindByIdAsync(student.PersonaId.ToString());
            if (persona is null)
            {
                _logger.LogInformation("Student account not found. Update not completed");
            }
            else
            {
                persona.FirstName = request.FirstName ?? persona.FirstName;
                persona.LastName = request.LastName ?? persona.LastName;
                await _userManager.UpdateAsync(persona);
            }

            return response;
        }


        public async Task<ApiResponse<List<StaffResponse>>> StaffListAsync()
        {
            var response = new ApiResponse<List<StaffResponse>>();

            var staff = _dbContext.Staffs
                .Select(x => new StaffResponse()
                {
                    StaffId = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    PhotoUrl = x.PhotoUrl,
                })
                .AsNoTracking();

            response.Data = await staff.ToListAsync();
            return response;
        }

        public async Task<ApiResponse<List<BusDriverResponse>>> BusDriverListAsync()
        {
            var response = new ApiResponse<List<BusDriverResponse>>();

            var busdrivers = _dbContext.Busdrivers
                .Include(x => x.Bus)
                .Select(x => new BusDriverResponse()
                {
                    BusDriverId = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    PhotoUrl = x.PhotoUrl,
                    Email = x.Email,
                    PhoneNumber = x.PhoneNumber,
                    BusId = x.BusId.Value,
                    BusNumber = x.Bus.Number,
                })
                .AsNoTracking();

            response.Data = await busdrivers.ToListAsync();
            return response;
        }

        public async Task<BaseResponse> DeleteParentAsync(Guid parentId, string deletor)
        {
            _logger.LogInformation("Trying to delete a parent with id: {0}", parentId);
            var response = new BaseResponse();

            var parent = await _dbContext.Parents.FirstOrDefaultAsync(x => x.Id == parentId);
            if (parent is null)
            {
                _logger.LogInformation("Parent with id: {0} not found", parentId);
                response.Code = ResponseCodes.Status404NotFound;
                response.Message = "Parent not found";
                response.Status = false;
                return response;
            }

            parent.Delete(deletor);

            if (!(await _dbContext.TrySaveChangesAsync()))
            {
                response.Code = ResponseCodes.Status500InternalServerError;
                response.Status = false;
                response.Message = "Unable to delete parent! Please try again";
                return response;
            }

            var persona = await _userManager.FindByIdAsync(parent.PersonaId.ToString());
            await _userManager.SetLockoutEnabledAsync(persona, true);
            await _userManager.SetLockoutEndDateAsync(persona, DateTimeOffset.MaxValue);
            await _userManager.UpdateAsync(persona);

            return response;
        }

        public async Task<ApiResponse<ParentResponse>> GetParentAsync(Guid parentId)
        {
            _logger.LogInformation("Trying to get a parent with id: {0}", parentId);
            var response = new ApiResponse<ParentResponse>() { Code = ResponseCodes.Status200OK };

            var parent = await _dbContext.Parents.FirstOrDefaultAsync(x => x.Id == parentId);
            if (parent is null)
            {
                _logger.LogInformation("Parent with id: {0} not found", parentId);
                response.Code = ResponseCodes.Status404NotFound;
                response.Message = "Parent not found";
                response.Status = false;
                return response;
            }

            response.Data = new ParentResponse()
            {
                Email = parent.Email ?? string.Empty,
                ParentId = parent.Id,
                PhoneNumber = parent.PhoneNumber ?? string.Empty,
                FirstName = parent.FirstName,
                LastName = parent.LastName,
                PhotoUrl = parent.PhotoUrl ?? string.Empty,
            };

            return response;
        }

        public async Task<BaseResponse> DeleteStudnetAsync(Guid studentId, string deletor)
        {
            _logger.LogInformation("Trying to delete a student with id: {0}", studentId);
            var response = new BaseResponse();

            var student = await _dbContext.Students.FirstOrDefaultAsync(x => x.Id == studentId);
            if (student is null)
            {
                _logger.LogInformation("Student with id: {0} not found", studentId);
                response.Code = ResponseCodes.Status404NotFound;
                response.Message = "Student not found";
                response.Status = false;
                return response;
            }

            student.Delete(deletor);
            var studentParent = await _dbContext.ParentStudent.FirstOrDefaultAsync(x => x.StudentsId == studentId);
            if (studentParent is null)
            {
                _logger.LogInformation("parent-student entity doesn't exist");
            }
            studentParent.IsDeleted = true;

            if (!(await _dbContext.TrySaveChangesAsync()))
            {
                response.Code = ResponseCodes.Status500InternalServerError;
                response.Status = false;
                response.Message = "Unable to delete student! Please try again";
                return response;
            }

            var persona = await _userManager.FindByIdAsync(student.PersonaId.ToString());
            persona.Delete(deletor);

            await _userManager.SetLockoutEnabledAsync(persona, true);
            await _userManager.SetLockoutEndDateAsync(persona, DateTimeOffset.MaxValue);
            await _userManager.UpdateAsync(persona);

            return response;
        }

        public async Task<ApiResponse<StudentResponse>> GetStudentAsync(Guid studentId)
        {
            _logger.LogInformation("Trying to get a student with id: {0}", studentId);
            var response = new ApiResponse<StudentResponse>() { Code = ResponseCodes.Status200OK };

            var student = await _dbContext.Students.FirstOrDefaultAsync(x => x.Id == studentId);
            if (student is null)
            {
                _logger.LogInformation("Student with id: {0} not found", studentId);
                response.Code = ResponseCodes.Status404NotFound;
                response.Message = "Student not found";
                response.Status = false;
                return response;
            }

            response.Data = new StudentResponse()
            {
                StudentId = student.Id,
                FirstName = student.FirstName,
                LastName = student.LastName,
                PhotoUrl = student.PhotoUrl ?? string.Empty,
            };

            return response;
        }

        public async Task<BaseResponse> DeletePersonaAccountAsync(string email)
        {
            _logger.LogInformation("Trying to delete a user account with email: {0}", email);
            var response = new BaseResponse();

            var persona = await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == email);
            if (persona is null)
            {
                _logger.LogInformation("User with email: {0} not found", email);
                response.Code = ResponseCodes.Status404NotFound;
                response.Message = "User not found";
                response.Status = false;
                return response;
            }

            persona.IsActive = false;
            persona.Delete(email);

            switch (persona.PesonaType)
            {
                case PersonaType.Admin:
                    break;

                case PersonaType.Parent:
                    {
                        var parent = await _dbContext.Parents.FirstOrDefaultAsync(x => x.PersonaId == persona.Id);
                        if (parent is null) break;

                        parent.Delete(persona.Email);
                        break;
                    }

                case PersonaType.Student:
                    {
                        var student = await _dbContext.Students.FirstOrDefaultAsync(x => x.PersonaId == persona.Id);
                        if (student is null) break;

                        student.Delete(persona.Email);
                        break;
                    }

                case PersonaType.BusDriver:
                    {
                        var busDriver = await _dbContext.Busdrivers.FirstOrDefaultAsync(x => x.PersonaId == persona.Id);
                        if (busDriver is null) break;

                        busDriver.Delete(persona.Email);
                        break;
                    }

                case PersonaType.Staff:
                    {
                        var staff = await _dbContext.Staffs.FirstOrDefaultAsync(x => x.PersonaId == persona.Id);
                        if (staff is null) break;

                        staff.Delete(persona.Email);
                        break;
                    }

                default:
                    break;
            }

            if (!(await _dbContext.TrySaveChangesAsync()))
            {
                response.Code = ResponseCodes.Status500InternalServerError;
                response.Status = false;
                response.Message = "Unable to delete user account! Please try again";
                return response;
            }

            return response;
        }

        public async Task<BaseResponse> UpdateParentInfoAsync(Guid parentId, UpdateParentInfoRequest request, string editor)
        {
            var response = new BaseResponse();

            var parent = await _dbContext.Parents.FirstOrDefaultAsync(x => x.Id == parentId);
            if (parent is null)
            {
                response.Code = ResponseCodes.Status404NotFound;
                response.Status = false;
                response.Message = "Parent not found";
                return response;
            }

            parent.FirstName = request.FirstName ?? parent.FirstName;
            parent.LastName = request.LastName ?? parent.LastName;
            parent.PhoneNumber = request.PhoneNumber ?? parent.PhoneNumber;
            parent.Edit(editor);

            if (!(await _dbContext.TrySaveChangesAsync()))
            {
                response.Code = ResponseCodes.Status500InternalServerError;
                response.Status = false;
                response.Message = "Unable to update parent information";
                return response;
            }

            var persona = await _userManager.FindByIdAsync(parent.PersonaId.ToString());
            if (persona is null)
            {
                _logger.LogInformation("Parent account not found. Update not completed");
                response.Code = ResponseCodes.Status500InternalServerError;
                response.Status = false;
                response.Message = "Unable to update because Parent User account was not found";
                return response;
            }
            else
            {
                persona.FirstName = request.FirstName ?? persona.FirstName;
                persona.LastName = request.LastName ?? persona.LastName;
                persona.PhoneNumber = request.PhoneNumber ?? persona.PhoneNumber;
                await _userManager.UpdateAsync(persona);
            }

            return response;
        }

        private string UploadPhoto(IFormFile? photo, string folder, string fileNameAlias)
        {
            string folderPath = "static";

            string path = Path.Combine(_webHost.ContentRootPath, folderPath, folder);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            if (photo != null && photo.Length > 0)
            {
                //Getting FileName
                var fileName = Path.GetFileName(photo.FileName);
                //Assigning Unique Filename (Guid)
                var myUniqueFileName = Convert.ToString(Guid.NewGuid());
                //Getting file Extension
                var fileExtension = Path.GetExtension(fileName);
                // concatenating  FileName + FileExtension
                var newFileName = String.Concat(fileNameAlias, myUniqueFileName, fileExtension);

                // Combines two strings into a path.
                string filepath = string.Empty;
                try
                {
                    filepath = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), folderPath, folder)).Root + $"{newFileName}";
                    using (FileStream fs = File.Create(filepath))
                    {
                        photo?.CopyTo(fs);
                        fs.Flush();
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogInformation("Error while uploading photo");
                    _logger.LogError(ex.Message);
                }

                string prefixToRemove = Path.Combine(Directory.GetCurrentDirectory());
                if (filepath.StartsWith(prefixToRemove))
                {
                    filepath = filepath.Substring(prefixToRemove.Length);
                    Console.WriteLine(filepath);
                }

                return filepath;
            }

            return string.Empty;
        }
    }
}
