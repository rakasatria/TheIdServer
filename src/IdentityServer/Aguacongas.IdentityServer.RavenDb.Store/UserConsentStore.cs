﻿// Project: Aguafrommars/TheIdServer
// Copyright (c) 2021 @Olivier Lefebvre
using Aguacongas.IdentityServer.Store.Entity;
using IdentityServer4.Models;
using IdentityServer4.Stores;
using IdentityServer4.Stores.Serialization;
using Microsoft.Extensions.Logging;
using Raven.Client.Documents.Session;
using System;
using System.Threading.Tasks;

namespace Aguacongas.IdentityServer.RavenDb.Store
{
    public class UserConsentStore : GrantStore<UserConsent, Consent>, IUserConsentStore
    {
        public UserConsentStore(IAsyncDocumentSession session, IPersistentGrantSerializer serializer, ILogger<UserConsentStore> logger)
            : base(session, serializer, logger)
        {
        }

        public Task<Consent> GetUserConsentAsync(string subjectId, string clientId)
            => GetAsync(subjectId, clientId);

        public Task RemoveUserConsentAsync(string subjectId, string clientId)
            => RemoveAsync(subjectId, clientId);

        public Task StoreUserConsentAsync(Consent consent)
            => StoreAsync(consent, consent.Expiration);

        protected override string GetClientId(Consent dto) 
            => dto?.ClientId;

        protected override string GetSubjectId(Consent dto) 
            => dto?.SubjectId;

        protected override DateTime? GetExpiration(Consent dto)
            => dto.Expiration;
    }
}
