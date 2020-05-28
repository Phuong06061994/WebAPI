select distinct 
		f.Id + '_' + a.Id
	from Permissions p 
	join Functions f on f.Id = p.FunctionId
	join Actions a on p.ActionId = a.Id
	join AspNetRoles r on p.RoleId = r.Id
	join AspNetUserRoles ur on r.Id = ur.RoleId
	where ur.UserId ='B23297EC-DC63-4D99-8EC4-B8C89EE30D16';
