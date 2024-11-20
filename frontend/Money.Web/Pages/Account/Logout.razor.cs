﻿using Microsoft.AspNetCore.Components;
using Money.Web.Services.Authentication;

namespace Money.Web.Pages.Account;

public partial class Logout
{
    [SupplyParameterFromQuery]
    private string? ReturnUrl { get; set; } = null;

    [Inject]
    private AuthenticationService AuthenticationService { get; set; } = default!;

    [Inject]
    private NavigationManager NavigationManager { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        await AuthenticationService.LogoutAsync();
        NavigationManager.ReturnTo(ReturnUrl, true);
    }
}
