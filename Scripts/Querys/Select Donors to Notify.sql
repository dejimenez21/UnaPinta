SELECT u.* FROM Users u
INNER JOIN UserRoles ur ON ur.UserId = u.Id AND ur.RoleId = 1
INNER JOIN Requests r ON r.Id = @requestId AND r.ProvinceId = u.ProvinceId
INNER JOIN RequestPossibleBloodTypes rp ON rp.RequestId = r.Id AND rp.BloodTypeId = u.BloodTypeId
WHERE u.EmailConfirmed = 1


