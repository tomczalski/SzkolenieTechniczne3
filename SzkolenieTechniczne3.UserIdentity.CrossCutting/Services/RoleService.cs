﻿using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SzkolenieTechniczne3.Common.CrossCutting.Exceptions;
using SzkolenieTechniczne3.Common.Storage.Interfaces;
using SzkolenieTechniczne3.UserIdentity.CrossCutting.Dtos;
using SzkolenieTechniczne3.UserIdentity.CrossCutting.Mapping;
using SzkolenieTechniczne3.UserIdentity.CrossCutting.Services.Interfaces;
using SzkolenieTechniczne3.UserIdentity.Storage.Entities;
using SzkolenieTechniczne3.UserIdentity.Storage.Migrations;

namespace SzkolenieTechniczne3.UserIdentity.CrossCutting.Services
{
    public class RoleService : IRoleService
    {
        private readonly UserIdentityDbContext _dbContext;
        private readonly RoleToRoleDtoMapping _entityToDtoMapping;
        private readonly RoleDtoToRoleMapping _dtoToEntityMapping;

        public RoleService(UserIdentityDbContext dbContext, RoleToRoleDtoMapping entityToDtoMapping, RoleDtoToRoleMapping dtoToEntityMapping)
        {
            _dbContext = dbContext;
            _entityToDtoMapping = entityToDtoMapping;
            _dtoToEntityMapping = dtoToEntityMapping;
        }

        public async Task AssignRolesToUser(Guid userId, Guid[] roleIds)
        {
            var user = await _dbContext
            .Users
            .Include(u => u.Roles)
            .SingleOrDefaultAsync(u => u.Id == userId);

            foreach (var roleIdToAssign in roleIds.Except(user.Roles.Select(r => r.Id)))
            {
                var roleToAssign = new Role { Id = roleIdToAssign };
                _dbContext.Attach(roleToAssign);
                user.Roles.Add(roleToAssign);
            }
            await _dbContext.SaveChangesAsync();
        }

        public async Task<RoleDto> Create(RoleDto dto)
        {
            var entity = _dtoToEntityMapping.Map(dto);
            _dbContext.Set<Role>().Add(entity);
            return dto;
        }

        public async Task<IEnumerable<RoleDto>> Read()
        {
            var entities = await _dbContext
            .Set<Role>()
            .AsNoTracking()
            .Select(_entityToDtoMapping.GetMapping())
            .ToListAsync();

            return entities;
        }

        public async Task<RoleDto> ReadById(Guid id)
        {
            var entity = await _dbContext
                .Set<Role>()
                .AsNoTracking()
                .Where(e => e.Id!.Equals(id))
                .Select(_entityToDtoMapping.GetMapping())
                .SingleOrDefaultAsync();

            if (entity == null) 
            {
                throw new ApiHttpException(StatusCodes.Status404NotFound);
            }
            return entity;
        }

        public async Task Delete(Guid id) 
        {
            var entity = await _dbContext
                .Set<Role>()
                .SingleOrDefaultAsync(e => e.Id!.Equals(id));

            if (entity == null)
            {
                throw new ApiHttpException(StatusCodes.Status404NotFound);
            }

            if (entity is ISoftDeletable softDeletebleEntity)
            {
                softDeletebleEntity.IsDeleted = true;

                _dbContext.Entry(softDeletebleEntity).State = EntityState.Modified;
            }
            else
            {
                _dbContext.Set<Role>().Remove(entity);
            }

            await _dbContext.SaveChangesAsync();
        }

        public async Task<RoleDto> Update(RoleDto dto) 
        {
            var dtoId = _dtoToEntityMapping.Map(dto).Id;

            if (!await _dbContext.Set<Role>().AnyAsync(e => e.Id!.Equals(dtoId))) 
            {
                throw new ApiHttpException(StatusCodes.Status404NotFound);
            }

            var entity = _dtoToEntityMapping.Map(dto);
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return dto;
        }
    }
}
