﻿using System;
using JetBrains.Annotations;
using Volo.Abp.Domain.Entities;

namespace Starshine.Authservice.Domain.Clients;

public class ClientScope : Entity<Guid>
{
    public virtual Guid ClientId { get; protected set; }

    public virtual string Scope { get; protected set; }

    protected ClientScope()
    {

    }

    public virtual bool Equals(Guid clientId, [NotNull] string scope)
    {
        return ClientId == clientId && Scope == scope;
    }

    protected internal ClientScope(Guid clientId, string scope)
    {
        ClientId = clientId;
        Scope = scope;
    }

    public override object[] GetKeys()
    {
        return new object[] { ClientId, Scope };
    }
}
