﻿using SignageLivePlayer.Api.Data.Models;

namespace SignageLivePlayer.Api.Authentication;

public record AuthenticationResult
(
    User User,
    string Token,
    bool Error,
    Exception Ex
);