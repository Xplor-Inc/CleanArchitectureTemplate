﻿namespace CleanArchitectureTemplate.WebApp.Models.Dtos.Accounts;

public class ChangePasswordDto
{
    public string OldPassword       { get; set; } = default!;
    public string NewPassword       { get; set; } = default!;
}