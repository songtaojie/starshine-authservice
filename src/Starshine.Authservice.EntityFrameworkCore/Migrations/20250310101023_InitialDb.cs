using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Starshine.Authservice.EntityFrameworkCore.Migrations
{
    /// <inheritdoc />
    public partial class InitialDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IdentityServerApiResource",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false),
                    name = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    display_name = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    description = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true),
                    enabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    allowed_access_token_signing_algorithms = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    show_in_discovery_document = table.Column<bool>(type: "INTEGER", nullable: false),
                    ExtraProperties = table.Column<string>(type: "TEXT", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatorId = table.Column<Guid>(type: "TEXT", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "TEXT", nullable: true),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "TEXT", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_identity_server_api_resource", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "IdentityServerApiScope",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false),
                    enabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    name = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    display_name = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    description = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true),
                    required = table.Column<bool>(type: "INTEGER", nullable: false),
                    emphasize = table.Column<bool>(type: "INTEGER", nullable: false),
                    show_in_discovery_document = table.Column<bool>(type: "INTEGER", nullable: false),
                    ExtraProperties = table.Column<string>(type: "TEXT", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatorId = table.Column<Guid>(type: "TEXT", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "TEXT", nullable: true),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "TEXT", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_identity_server_api_scope", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "IdentityServerClient",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false),
                    client_id = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    client_name = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    description = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true),
                    client_uri = table.Column<string>(type: "TEXT", maxLength: 2000, nullable: true),
                    logo_uri = table.Column<string>(type: "TEXT", maxLength: 2000, nullable: true),
                    enabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    protocol_type = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    require_client_secret = table.Column<bool>(type: "INTEGER", nullable: false),
                    require_consent = table.Column<bool>(type: "INTEGER", nullable: false),
                    allow_remember_consent = table.Column<bool>(type: "INTEGER", nullable: false),
                    always_include_user_claims_in_id_token = table.Column<bool>(type: "INTEGER", nullable: false),
                    require_pkce = table.Column<bool>(type: "INTEGER", nullable: false),
                    allow_plain_text_pkce = table.Column<bool>(type: "INTEGER", nullable: false),
                    require_request_object = table.Column<bool>(type: "INTEGER", nullable: false),
                    allow_access_tokens_via_browser = table.Column<bool>(type: "INTEGER", nullable: false),
                    front_channel_logout_uri = table.Column<string>(type: "TEXT", maxLength: 2000, nullable: true),
                    front_channel_logout_session_required = table.Column<bool>(type: "INTEGER", nullable: false),
                    back_channel_logout_uri = table.Column<string>(type: "TEXT", maxLength: 2000, nullable: true),
                    back_channel_logout_session_required = table.Column<bool>(type: "INTEGER", nullable: false),
                    allow_offline_access = table.Column<bool>(type: "INTEGER", nullable: false),
                    identity_token_lifetime = table.Column<int>(type: "INTEGER", nullable: false),
                    allowed_identity_token_signing_algorithms = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    access_token_lifetime = table.Column<int>(type: "INTEGER", nullable: false),
                    authorization_code_lifetime = table.Column<int>(type: "INTEGER", nullable: false),
                    consent_lifetime = table.Column<int>(type: "INTEGER", nullable: true),
                    absolute_refresh_token_lifetime = table.Column<int>(type: "INTEGER", nullable: false),
                    sliding_refresh_token_lifetime = table.Column<int>(type: "INTEGER", nullable: false),
                    refresh_token_usage = table.Column<int>(type: "INTEGER", nullable: false),
                    update_access_token_claims_on_refresh = table.Column<bool>(type: "INTEGER", nullable: false),
                    refresh_token_expiration = table.Column<int>(type: "INTEGER", nullable: false),
                    access_token_type = table.Column<int>(type: "INTEGER", nullable: false),
                    enable_local_login = table.Column<bool>(type: "INTEGER", nullable: false),
                    include_jwt_id = table.Column<bool>(type: "INTEGER", nullable: false),
                    always_send_client_claims = table.Column<bool>(type: "INTEGER", nullable: false),
                    client_claims_prefix = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    pair_wise_subject_salt = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    user_sso_lifetime = table.Column<int>(type: "INTEGER", nullable: true),
                    user_code_type = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    device_code_lifetime = table.Column<int>(type: "INTEGER", nullable: false),
                    ExtraProperties = table.Column<string>(type: "TEXT", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatorId = table.Column<Guid>(type: "TEXT", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "TEXT", nullable: true),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "TEXT", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_identity_server_client", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "IdentityServerDeviceFlowCodes",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false),
                    device_code = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    user_code = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    subject_id = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    session_id = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    client_id = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    description = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    expiration = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    data = table.Column<string>(type: "TEXT", maxLength: 50000, nullable: false),
                    ExtraProperties = table.Column<string>(type: "TEXT", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatorId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_identity_server_device_flow_codes", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "IdentityServerIdentityResource",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false),
                    name = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    display_name = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    description = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true),
                    enabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    required = table.Column<bool>(type: "INTEGER", nullable: false),
                    emphasize = table.Column<bool>(type: "INTEGER", nullable: false),
                    show_in_discovery_document = table.Column<bool>(type: "INTEGER", nullable: false),
                    ExtraProperties = table.Column<string>(type: "TEXT", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatorId = table.Column<Guid>(type: "TEXT", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "TEXT", nullable: true),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "TEXT", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_identity_server_identity_resource", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "IdentityServerPersistedGrant",
                columns: table => new
                {
                    key = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    type = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    subject_id = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    session_id = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    client_id = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    description = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    creation_time = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    expiration = table.Column<DateTimeOffset>(type: "TEXT", nullable: true),
                    consumed_time = table.Column<DateTimeOffset>(type: "TEXT", nullable: true),
                    data = table.Column<string>(type: "TEXT", maxLength: 50000, nullable: false),
                    id = table.Column<Guid>(type: "TEXT", nullable: false),
                    ExtraProperties = table.Column<string>(type: "TEXT", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_identity_server_persisted_grant", x => x.key);
                });

            migrationBuilder.CreateTable(
                name: "StarshineIdentityClaimType",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false),
                    name = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false),
                    required = table.Column<bool>(type: "INTEGER", nullable: false),
                    is_static = table.Column<bool>(type: "INTEGER", nullable: false),
                    regex = table.Column<string>(type: "TEXT", maxLength: 512, nullable: true),
                    regex_description = table.Column<string>(type: "TEXT", maxLength: 128, nullable: true),
                    description = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    value_type = table.Column<int>(type: "INTEGER", nullable: false),
                    ExtraProperties = table.Column<string>(type: "TEXT", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_starshine_identity_claim_type", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "StarshineIdentityLinkUser",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false),
                    source_user_id = table.Column<Guid>(type: "TEXT", nullable: false),
                    source_tenant_id = table.Column<Guid>(type: "TEXT", nullable: true),
                    target_user_id = table.Column<Guid>(type: "TEXT", nullable: false),
                    target_tenant_id = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_starshine_identity_link_user", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "StarshineIdentityRole",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false),
                    TenantId = table.Column<Guid>(type: "TEXT", nullable: true),
                    name = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false),
                    normalized_name = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false),
                    is_default = table.Column<bool>(type: "INTEGER", nullable: false),
                    is_static = table.Column<bool>(type: "INTEGER", nullable: false),
                    is_public = table.Column<bool>(type: "INTEGER", nullable: false),
                    entity_version = table.Column<int>(type: "INTEGER", nullable: false),
                    ExtraProperties = table.Column<string>(type: "TEXT", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_starshine_identity_role", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "StarshineIdentitySecurityLog",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false),
                    TenantId = table.Column<Guid>(type: "TEXT", nullable: true),
                    application_name = table.Column<string>(type: "TEXT", maxLength: 96, nullable: true),
                    identity = table.Column<string>(type: "TEXT", maxLength: 96, nullable: true),
                    action = table.Column<string>(type: "TEXT", maxLength: 96, nullable: true),
                    user_id = table.Column<Guid>(type: "TEXT", nullable: true),
                    user_name = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    tenant_name = table.Column<string>(type: "TEXT", maxLength: 64, nullable: true),
                    client_id = table.Column<string>(type: "TEXT", maxLength: 64, nullable: false),
                    correlation_id = table.Column<string>(type: "TEXT", maxLength: 64, nullable: true),
                    client_ip_address = table.Column<string>(type: "TEXT", maxLength: 64, nullable: true),
                    browser_info = table.Column<string>(type: "TEXT", maxLength: 512, nullable: true),
                    creation_time = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ExtraProperties = table.Column<string>(type: "TEXT", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_starshine_identity_security_log", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "StarshineIdentitySession",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false),
                    session_id = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    device = table.Column<string>(type: "TEXT", maxLength: 64, nullable: false),
                    device_info = table.Column<string>(type: "TEXT", maxLength: 64, nullable: true),
                    TenantId = table.Column<Guid>(type: "TEXT", nullable: true),
                    user_id = table.Column<Guid>(type: "TEXT", nullable: false),
                    client_id = table.Column<string>(type: "TEXT", maxLength: 64, nullable: false),
                    ip_addresses = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    signed_in = table.Column<DateTime>(type: "TEXT", nullable: false),
                    last_accessed = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_starshine_identity_session", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "StarshineIdentityUser",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false),
                    TenantId = table.Column<Guid>(type: "TEXT", nullable: true),
                    user_name = table.Column<string>(type: "TEXT", maxLength: 64, nullable: false),
                    normalized_user_name = table.Column<string>(type: "TEXT", maxLength: 64, nullable: false),
                    name = table.Column<string>(type: "TEXT", maxLength: 64, nullable: true),
                    surname = table.Column<string>(type: "TEXT", maxLength: 64, nullable: true),
                    email = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    normalized_email = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    email_confirmed = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                    password_hash = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    security_stamp = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false),
                    is_external = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                    phone_number = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    phone_number_confirmed = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                    is_active = table.Column<bool>(type: "INTEGER", nullable: false),
                    two_factor_enabled = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                    lockout_end = table.Column<DateTimeOffset>(type: "TEXT", nullable: true),
                    lockout_enabled = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                    access_failed_count = table.Column<int>(type: "INTEGER", nullable: false, defaultValue: 0),
                    should_change_password_on_next_login = table.Column<bool>(type: "INTEGER", nullable: false),
                    entity_version = table.Column<int>(type: "INTEGER", nullable: false),
                    last_password_change_time = table.Column<DateTimeOffset>(type: "TEXT", nullable: true),
                    ExtraProperties = table.Column<string>(type: "TEXT", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatorId = table.Column<Guid>(type: "TEXT", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "TEXT", nullable: true),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "TEXT", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_starshine_identity_user", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "StarshineIdentityUserDelegation",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false),
                    TenantId = table.Column<Guid>(type: "TEXT", nullable: true),
                    source_user_id = table.Column<Guid>(type: "TEXT", nullable: false),
                    target_user_id = table.Column<Guid>(type: "TEXT", nullable: false),
                    start_time = table.Column<DateTime>(type: "TEXT", nullable: false),
                    end_time = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_starshine_identity_user_delegation", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "StarshineOrganizationUnit",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false),
                    TenantId = table.Column<Guid>(type: "TEXT", nullable: true),
                    parent_id = table.Column<Guid>(type: "TEXT", nullable: true),
                    code = table.Column<string>(type: "TEXT", maxLength: 95, nullable: false),
                    display_name = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    entity_version = table.Column<int>(type: "INTEGER", nullable: false),
                    ExtraProperties = table.Column<string>(type: "TEXT", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatorId = table.Column<Guid>(type: "TEXT", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "TEXT", nullable: true),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "TEXT", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_starshine_organization_unit", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "StarshinePermissionGrants",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false),
                    TenantId = table.Column<Guid>(type: "TEXT", nullable: true),
                    name = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    provider_name = table.Column<string>(type: "TEXT", maxLength: 64, nullable: false),
                    provider_key = table.Column<string>(type: "TEXT", maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_starshine_permission_grants", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "StarshinePermissionGroups",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false),
                    name = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    display_name = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false),
                    ExtraProperties = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_starshine_permission_groups", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "StarshinePermissions",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false),
                    group_name = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    name = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    parent_name = table.Column<string>(type: "TEXT", maxLength: 128, nullable: true),
                    display_name = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false),
                    is_enabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    multi_tenancy_side = table.Column<byte>(type: "INTEGER", nullable: false),
                    providers = table.Column<string>(type: "TEXT", maxLength: 128, nullable: true),
                    state_checkers = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    ExtraProperties = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_starshine_permissions", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "StarshineTenants",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false),
                    name = table.Column<string>(type: "TEXT", maxLength: 64, nullable: false),
                    normalized_name = table.Column<string>(type: "TEXT", maxLength: 64, nullable: false),
                    entity_version = table.Column<int>(type: "INTEGER", nullable: false),
                    ExtraProperties = table.Column<string>(type: "TEXT", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatorId = table.Column<Guid>(type: "TEXT", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "TEXT", nullable: true),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "TEXT", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_starshine_tenants", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "IdentityServerApiResourceClaim",
                columns: table => new
                {
                    type = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    api_resource_id = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_identity_server_api_resource_claim", x => new { x.api_resource_id, x.type });
                    table.ForeignKey(
                        name: "fk_identity_server_api_resource_claim_identity_server_api_resource_api_resource_id",
                        column: x => x.api_resource_id,
                        principalTable: "IdentityServerApiResource",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IdentityServerApiResourceProperty",
                columns: table => new
                {
                    api_resource_id = table.Column<Guid>(type: "TEXT", nullable: false),
                    key = table.Column<string>(type: "TEXT", maxLength: 250, nullable: false),
                    value = table.Column<string>(type: "TEXT", maxLength: 2000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_identity_server_api_resource_property", x => new { x.api_resource_id, x.key, x.value });
                    table.ForeignKey(
                        name: "fk_identity_server_api_resource_property_identity_server_api_resource_api_resource_id",
                        column: x => x.api_resource_id,
                        principalTable: "IdentityServerApiResource",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IdentityServerApiResourceScope",
                columns: table => new
                {
                    api_resource_id = table.Column<Guid>(type: "TEXT", nullable: false),
                    scope = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_identity_server_api_resource_scope", x => new { x.api_resource_id, x.scope });
                    table.ForeignKey(
                        name: "fk_identity_server_api_resource_scope_identity_server_api_resource_api_resource_id",
                        column: x => x.api_resource_id,
                        principalTable: "IdentityServerApiResource",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IdentityServerApiResourceSecret",
                columns: table => new
                {
                    type = table.Column<string>(type: "TEXT", maxLength: 250, nullable: false),
                    value = table.Column<string>(type: "TEXT", maxLength: 4000, nullable: false),
                    api_resource_id = table.Column<Guid>(type: "TEXT", nullable: false),
                    description = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true),
                    expiration = table.Column<DateTimeOffset>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_identity_server_api_resource_secret", x => new { x.api_resource_id, x.type, x.value });
                    table.ForeignKey(
                        name: "fk_identity_server_api_resource_secret_identity_server_api_resource_api_resource_id",
                        column: x => x.api_resource_id,
                        principalTable: "IdentityServerApiResource",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IdentityServerApiScopeClaim",
                columns: table => new
                {
                    type = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    api_scope_id = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_identity_server_api_scope_claim", x => new { x.api_scope_id, x.type });
                    table.ForeignKey(
                        name: "fk_identity_server_api_scope_claim_identity_server_api_scope_api_scope_id",
                        column: x => x.api_scope_id,
                        principalTable: "IdentityServerApiScope",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IdentityServerApiScopeProperty",
                columns: table => new
                {
                    api_scope_id = table.Column<Guid>(type: "TEXT", nullable: false),
                    key = table.Column<string>(type: "TEXT", maxLength: 250, nullable: false),
                    value = table.Column<string>(type: "TEXT", maxLength: 2000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_identity_server_api_scope_property", x => new { x.api_scope_id, x.key, x.value });
                    table.ForeignKey(
                        name: "fk_identity_server_api_scope_property_identity_server_api_scope_api_scope_id",
                        column: x => x.api_scope_id,
                        principalTable: "IdentityServerApiScope",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IdentityServerClientClaim",
                columns: table => new
                {
                    client_id = table.Column<Guid>(type: "TEXT", nullable: false),
                    type = table.Column<string>(type: "TEXT", maxLength: 250, nullable: false),
                    value = table.Column<string>(type: "TEXT", maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_identity_server_client_claim", x => new { x.client_id, x.type, x.value });
                    table.ForeignKey(
                        name: "fk_identity_server_client_claim_identity_server_client_client_id",
                        column: x => x.client_id,
                        principalTable: "IdentityServerClient",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IdentityServerClientCorsOrigin",
                columns: table => new
                {
                    client_id = table.Column<Guid>(type: "TEXT", nullable: false),
                    origin = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_identity_server_client_cors_origin", x => new { x.client_id, x.origin });
                    table.ForeignKey(
                        name: "fk_identity_server_client_cors_origin_identity_server_client_client_id",
                        column: x => x.client_id,
                        principalTable: "IdentityServerClient",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IdentityServerClientGrantType",
                columns: table => new
                {
                    client_id = table.Column<Guid>(type: "TEXT", nullable: false),
                    grant_type = table.Column<string>(type: "TEXT", maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_identity_server_client_grant_type", x => new { x.client_id, x.grant_type });
                    table.ForeignKey(
                        name: "fk_identity_server_client_grant_type_identity_server_client_client_id",
                        column: x => x.client_id,
                        principalTable: "IdentityServerClient",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IdentityServerClientIdPRestriction",
                columns: table => new
                {
                    client_id = table.Column<Guid>(type: "TEXT", nullable: false),
                    provider = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_identity_server_client_id_p_restriction", x => new { x.client_id, x.provider });
                    table.ForeignKey(
                        name: "fk_identity_server_client_id_p_restriction_identity_server_client_client_id",
                        column: x => x.client_id,
                        principalTable: "IdentityServerClient",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IdentityServerClientPostLogoutRedirectUri",
                columns: table => new
                {
                    client_id = table.Column<Guid>(type: "TEXT", nullable: false),
                    post_logout_redirect_uri = table.Column<string>(type: "TEXT", maxLength: 2000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_identity_server_client_post_logout_redirect_uri", x => new { x.client_id, x.post_logout_redirect_uri });
                    table.ForeignKey(
                        name: "fk_identity_server_client_post_logout_redirect_uri_identity_server_client_client_id",
                        column: x => x.client_id,
                        principalTable: "IdentityServerClient",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IdentityServerClientProperty",
                columns: table => new
                {
                    client_id = table.Column<Guid>(type: "TEXT", nullable: false),
                    key = table.Column<string>(type: "TEXT", maxLength: 250, nullable: false),
                    value = table.Column<string>(type: "TEXT", maxLength: 2000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_identity_server_client_property", x => new { x.client_id, x.key, x.value });
                    table.ForeignKey(
                        name: "fk_identity_server_client_property_identity_server_client_client_id",
                        column: x => x.client_id,
                        principalTable: "IdentityServerClient",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IdentityServerClientRedirectUri",
                columns: table => new
                {
                    client_id = table.Column<Guid>(type: "TEXT", nullable: false),
                    redirect_uri = table.Column<string>(type: "TEXT", maxLength: 2000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_identity_server_client_redirect_uri", x => new { x.client_id, x.redirect_uri });
                    table.ForeignKey(
                        name: "fk_identity_server_client_redirect_uri_identity_server_client_client_id",
                        column: x => x.client_id,
                        principalTable: "IdentityServerClient",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IdentityServerClientScope",
                columns: table => new
                {
                    client_id = table.Column<Guid>(type: "TEXT", nullable: false),
                    scope = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_identity_server_client_scope", x => new { x.client_id, x.scope });
                    table.ForeignKey(
                        name: "fk_identity_server_client_scope_identity_server_client_client_id",
                        column: x => x.client_id,
                        principalTable: "IdentityServerClient",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IdentityServerClientSecret",
                columns: table => new
                {
                    type = table.Column<string>(type: "TEXT", maxLength: 250, nullable: false),
                    value = table.Column<string>(type: "TEXT", maxLength: 4000, nullable: false),
                    client_id = table.Column<Guid>(type: "TEXT", nullable: false),
                    description = table.Column<string>(type: "TEXT", maxLength: 2000, nullable: true),
                    expiration = table.Column<DateTimeOffset>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_identity_server_client_secret", x => new { x.client_id, x.type, x.value });
                    table.ForeignKey(
                        name: "fk_identity_server_client_secret_identity_server_client_client_id",
                        column: x => x.client_id,
                        principalTable: "IdentityServerClient",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IdentityServerIdentityResourceClaim",
                columns: table => new
                {
                    type = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    identity_resource_id = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_identity_server_identity_resource_claim", x => new { x.identity_resource_id, x.type });
                    table.ForeignKey(
                        name: "fk_identity_server_identity_resource_claim_identity_server_identity_resource_identity_resource_id",
                        column: x => x.identity_resource_id,
                        principalTable: "IdentityServerIdentityResource",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IdentityServerIdentityResourceProperty",
                columns: table => new
                {
                    identity_resource_id = table.Column<Guid>(type: "TEXT", nullable: false),
                    key = table.Column<string>(type: "TEXT", maxLength: 250, nullable: false),
                    value = table.Column<string>(type: "TEXT", maxLength: 2000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_identity_server_identity_resource_property", x => new { x.identity_resource_id, x.key, x.value });
                    table.ForeignKey(
                        name: "fk_identity_server_identity_resource_property_identity_server_identity_resource_identity_resource_id",
                        column: x => x.identity_resource_id,
                        principalTable: "IdentityServerIdentityResource",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StarshineIdentityRoleClaim",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false),
                    role_id = table.Column<Guid>(type: "TEXT", nullable: false),
                    identity_role_id = table.Column<Guid>(type: "TEXT", nullable: true),
                    TenantId = table.Column<Guid>(type: "TEXT", nullable: true),
                    claim_type = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false),
                    claim_value = table.Column<string>(type: "TEXT", maxLength: 1024, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_starshine_identity_role_claim", x => x.id);
                    table.ForeignKey(
                        name: "fk_starshine_identity_role_claim_starshine_identity_role_identity_role_id",
                        column: x => x.identity_role_id,
                        principalTable: "StarshineIdentityRole",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "StarshineIdentityUserClaim",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false),
                    user_id = table.Column<Guid>(type: "TEXT", nullable: false),
                    identity_user_id = table.Column<Guid>(type: "TEXT", nullable: true),
                    TenantId = table.Column<Guid>(type: "TEXT", nullable: true),
                    claim_type = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false),
                    claim_value = table.Column<string>(type: "TEXT", maxLength: 1024, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_starshine_identity_user_claim", x => x.id);
                    table.ForeignKey(
                        name: "fk_starshine_identity_user_claim_starshine_identity_user_identity_user_id",
                        column: x => x.identity_user_id,
                        principalTable: "StarshineIdentityUser",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "StarshineIdentityUserLogin",
                columns: table => new
                {
                    user_id = table.Column<Guid>(type: "TEXT", nullable: false),
                    login_provider = table.Column<string>(type: "TEXT", maxLength: 64, nullable: false),
                    TenantId = table.Column<Guid>(type: "TEXT", nullable: true),
                    provider_key = table.Column<string>(type: "TEXT", maxLength: 196, nullable: false),
                    provider_display_name = table.Column<string>(type: "TEXT", maxLength: 128, nullable: true),
                    identity_user_id = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_starshine_identity_user_login", x => new { x.user_id, x.login_provider });
                    table.ForeignKey(
                        name: "fk_starshine_identity_user_login_starshine_identity_user_identity_user_id",
                        column: x => x.identity_user_id,
                        principalTable: "StarshineIdentityUser",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "StarshineIdentityUserOrganizationUnit",
                columns: table => new
                {
                    user_id = table.Column<Guid>(type: "TEXT", nullable: false),
                    organization_unit_id = table.Column<Guid>(type: "TEXT", nullable: false),
                    TenantId = table.Column<Guid>(type: "TEXT", nullable: true),
                    identity_user_id = table.Column<Guid>(type: "TEXT", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatorId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_starshine_identity_user_organization_unit", x => new { x.organization_unit_id, x.user_id });
                    table.ForeignKey(
                        name: "fk_starshine_identity_user_organization_unit_starshine_identity_user_identity_user_id",
                        column: x => x.identity_user_id,
                        principalTable: "StarshineIdentityUser",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "StarshineIdentityUserRole",
                columns: table => new
                {
                    user_id = table.Column<Guid>(type: "TEXT", nullable: false),
                    role_id = table.Column<Guid>(type: "TEXT", nullable: false),
                    TenantId = table.Column<Guid>(type: "TEXT", nullable: true),
                    identity_user_id = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_starshine_identity_user_role", x => new { x.user_id, x.role_id });
                    table.ForeignKey(
                        name: "fk_starshine_identity_user_role_starshine_identity_user_identity_user_id",
                        column: x => x.identity_user_id,
                        principalTable: "StarshineIdentityUser",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "StarshineIdentityUserToken",
                columns: table => new
                {
                    user_id = table.Column<Guid>(type: "TEXT", nullable: false),
                    login_provider = table.Column<string>(type: "TEXT", maxLength: 64, nullable: false),
                    name = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    TenantId = table.Column<Guid>(type: "TEXT", nullable: true),
                    value = table.Column<string>(type: "TEXT", nullable: true),
                    identity_user_id = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_starshine_identity_user_token", x => new { x.user_id, x.login_provider, x.name });
                    table.ForeignKey(
                        name: "fk_starshine_identity_user_token_starshine_identity_user_identity_user_id",
                        column: x => x.identity_user_id,
                        principalTable: "StarshineIdentityUser",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "StarshineOrganizationUnitRole",
                columns: table => new
                {
                    role_id = table.Column<Guid>(type: "TEXT", nullable: false),
                    organization_unit_id = table.Column<Guid>(type: "TEXT", nullable: false),
                    TenantId = table.Column<Guid>(type: "TEXT", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatorId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_starshine_organization_unit_role", x => new { x.organization_unit_id, x.role_id });
                    table.ForeignKey(
                        name: "fk_starshine_organization_unit_role_starshine_organization_unit_organization_unit_id",
                        column: x => x.organization_unit_id,
                        principalTable: "StarshineOrganizationUnit",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StarshineTenantConnectionStrings",
                columns: table => new
                {
                    tenant_id = table.Column<Guid>(type: "TEXT", nullable: false),
                    name = table.Column<string>(type: "TEXT", maxLength: 64, nullable: false),
                    value = table.Column<string>(type: "TEXT", maxLength: 1024, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_starshine_tenant_connection_strings", x => new { x.tenant_id, x.name });
                    table.ForeignKey(
                        name: "fk_starshine_tenant_connection_strings_starshine_tenants_tenant_id",
                        column: x => x.tenant_id,
                        principalTable: "StarshineTenants",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_identity_server_client_client_id",
                table: "IdentityServerClient",
                column: "client_id");

            migrationBuilder.CreateIndex(
                name: "ix_identity_server_device_flow_codes_device_code",
                table: "IdentityServerDeviceFlowCodes",
                column: "device_code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_identity_server_device_flow_codes_expiration",
                table: "IdentityServerDeviceFlowCodes",
                column: "expiration");

            migrationBuilder.CreateIndex(
                name: "ix_identity_server_device_flow_codes_user_code",
                table: "IdentityServerDeviceFlowCodes",
                column: "user_code");

            migrationBuilder.CreateIndex(
                name: "ix_identity_server_persisted_grant_expiration",
                table: "IdentityServerPersistedGrant",
                column: "expiration");

            migrationBuilder.CreateIndex(
                name: "ix_identity_server_persisted_grant_subject_id_client_id_type",
                table: "IdentityServerPersistedGrant",
                columns: new[] { "subject_id", "client_id", "type" });

            migrationBuilder.CreateIndex(
                name: "ix_identity_server_persisted_grant_subject_id_session_id_type",
                table: "IdentityServerPersistedGrant",
                columns: new[] { "subject_id", "session_id", "type" });

            migrationBuilder.CreateIndex(
                name: "ix_starshine_identity_link_user_source_user_id_source_tenant_id_target_user_id_target_tenant_id",
                table: "StarshineIdentityLinkUser",
                columns: new[] { "source_user_id", "source_tenant_id", "target_user_id", "target_tenant_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_starshine_identity_role_name",
                table: "StarshineIdentityRole",
                column: "name");

            migrationBuilder.CreateIndex(
                name: "ix_starshine_identity_role_normalized_name",
                table: "StarshineIdentityRole",
                column: "normalized_name");

            migrationBuilder.CreateIndex(
                name: "ix_starshine_identity_role_claim_identity_role_id",
                table: "StarshineIdentityRoleClaim",
                column: "identity_role_id");

            migrationBuilder.CreateIndex(
                name: "ix_starshine_identity_role_claim_role_id",
                table: "StarshineIdentityRoleClaim",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "ix_starshine_identity_security_log_tenant_id_action",
                table: "StarshineIdentitySecurityLog",
                columns: new[] { "TenantId", "action" });

            migrationBuilder.CreateIndex(
                name: "ix_starshine_identity_security_log_tenant_id_application_name",
                table: "StarshineIdentitySecurityLog",
                columns: new[] { "TenantId", "application_name" });

            migrationBuilder.CreateIndex(
                name: "ix_starshine_identity_security_log_tenant_id_identity",
                table: "StarshineIdentitySecurityLog",
                columns: new[] { "TenantId", "identity" });

            migrationBuilder.CreateIndex(
                name: "ix_starshine_identity_security_log_tenant_id_user_id",
                table: "StarshineIdentitySecurityLog",
                columns: new[] { "TenantId", "user_id" });

            migrationBuilder.CreateIndex(
                name: "ix_starshine_identity_session_device",
                table: "StarshineIdentitySession",
                column: "device");

            migrationBuilder.CreateIndex(
                name: "ix_starshine_identity_session_session_id",
                table: "StarshineIdentitySession",
                column: "session_id");

            migrationBuilder.CreateIndex(
                name: "ix_starshine_identity_session_tenant_id_user_id",
                table: "StarshineIdentitySession",
                columns: new[] { "TenantId", "user_id" });

            migrationBuilder.CreateIndex(
                name: "ix_starshine_identity_user_email",
                table: "StarshineIdentityUser",
                column: "email");

            migrationBuilder.CreateIndex(
                name: "ix_starshine_identity_user_normalized_email",
                table: "StarshineIdentityUser",
                column: "normalized_email");

            migrationBuilder.CreateIndex(
                name: "ix_starshine_identity_user_normalized_user_name",
                table: "StarshineIdentityUser",
                column: "normalized_user_name");

            migrationBuilder.CreateIndex(
                name: "ix_starshine_identity_user_user_name",
                table: "StarshineIdentityUser",
                column: "user_name");

            migrationBuilder.CreateIndex(
                name: "ix_starshine_identity_user_claim_identity_user_id",
                table: "StarshineIdentityUserClaim",
                column: "identity_user_id");

            migrationBuilder.CreateIndex(
                name: "ix_starshine_identity_user_claim_user_id",
                table: "StarshineIdentityUserClaim",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_starshine_identity_user_login_identity_user_id",
                table: "StarshineIdentityUserLogin",
                column: "identity_user_id");

            migrationBuilder.CreateIndex(
                name: "ix_starshine_identity_user_login_login_provider_provider_key",
                table: "StarshineIdentityUserLogin",
                columns: new[] { "login_provider", "provider_key" });

            migrationBuilder.CreateIndex(
                name: "ix_starshine_identity_user_organization_unit_identity_user_id",
                table: "StarshineIdentityUserOrganizationUnit",
                column: "identity_user_id");

            migrationBuilder.CreateIndex(
                name: "ix_starshine_identity_user_organization_unit_user_id_organization_unit_id",
                table: "StarshineIdentityUserOrganizationUnit",
                columns: new[] { "user_id", "organization_unit_id" });

            migrationBuilder.CreateIndex(
                name: "ix_starshine_identity_user_role_identity_user_id",
                table: "StarshineIdentityUserRole",
                column: "identity_user_id");

            migrationBuilder.CreateIndex(
                name: "ix_starshine_identity_user_role_role_id_user_id",
                table: "StarshineIdentityUserRole",
                columns: new[] { "role_id", "user_id" });

            migrationBuilder.CreateIndex(
                name: "ix_starshine_identity_user_token_identity_user_id",
                table: "StarshineIdentityUserToken",
                column: "identity_user_id");

            migrationBuilder.CreateIndex(
                name: "ix_starshine_organization_unit_code",
                table: "StarshineOrganizationUnit",
                column: "code");

            migrationBuilder.CreateIndex(
                name: "ix_starshine_organization_unit_role_role_id_organization_unit_id",
                table: "StarshineOrganizationUnitRole",
                columns: new[] { "role_id", "organization_unit_id" });

            migrationBuilder.CreateIndex(
                name: "ix_starshine_permission_grants_tenant_id_name_provider_name_provider_key",
                table: "StarshinePermissionGrants",
                columns: new[] { "TenantId", "name", "provider_name", "provider_key" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_starshine_permission_groups_name",
                table: "StarshinePermissionGroups",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_starshine_permissions_group_name",
                table: "StarshinePermissions",
                column: "group_name");

            migrationBuilder.CreateIndex(
                name: "ix_starshine_permissions_name",
                table: "StarshinePermissions",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_starshine_tenants_name",
                table: "StarshineTenants",
                column: "name");

            migrationBuilder.CreateIndex(
                name: "ix_starshine_tenants_normalized_name",
                table: "StarshineTenants",
                column: "normalized_name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IdentityServerApiResourceClaim");

            migrationBuilder.DropTable(
                name: "IdentityServerApiResourceProperty");

            migrationBuilder.DropTable(
                name: "IdentityServerApiResourceScope");

            migrationBuilder.DropTable(
                name: "IdentityServerApiResourceSecret");

            migrationBuilder.DropTable(
                name: "IdentityServerApiScopeClaim");

            migrationBuilder.DropTable(
                name: "IdentityServerApiScopeProperty");

            migrationBuilder.DropTable(
                name: "IdentityServerClientClaim");

            migrationBuilder.DropTable(
                name: "IdentityServerClientCorsOrigin");

            migrationBuilder.DropTable(
                name: "IdentityServerClientGrantType");

            migrationBuilder.DropTable(
                name: "IdentityServerClientIdPRestriction");

            migrationBuilder.DropTable(
                name: "IdentityServerClientPostLogoutRedirectUri");

            migrationBuilder.DropTable(
                name: "IdentityServerClientProperty");

            migrationBuilder.DropTable(
                name: "IdentityServerClientRedirectUri");

            migrationBuilder.DropTable(
                name: "IdentityServerClientScope");

            migrationBuilder.DropTable(
                name: "IdentityServerClientSecret");

            migrationBuilder.DropTable(
                name: "IdentityServerDeviceFlowCodes");

            migrationBuilder.DropTable(
                name: "IdentityServerIdentityResourceClaim");

            migrationBuilder.DropTable(
                name: "IdentityServerIdentityResourceProperty");

            migrationBuilder.DropTable(
                name: "IdentityServerPersistedGrant");

            migrationBuilder.DropTable(
                name: "StarshineIdentityClaimType");

            migrationBuilder.DropTable(
                name: "StarshineIdentityLinkUser");

            migrationBuilder.DropTable(
                name: "StarshineIdentityRoleClaim");

            migrationBuilder.DropTable(
                name: "StarshineIdentitySecurityLog");

            migrationBuilder.DropTable(
                name: "StarshineIdentitySession");

            migrationBuilder.DropTable(
                name: "StarshineIdentityUserClaim");

            migrationBuilder.DropTable(
                name: "StarshineIdentityUserDelegation");

            migrationBuilder.DropTable(
                name: "StarshineIdentityUserLogin");

            migrationBuilder.DropTable(
                name: "StarshineIdentityUserOrganizationUnit");

            migrationBuilder.DropTable(
                name: "StarshineIdentityUserRole");

            migrationBuilder.DropTable(
                name: "StarshineIdentityUserToken");

            migrationBuilder.DropTable(
                name: "StarshineOrganizationUnitRole");

            migrationBuilder.DropTable(
                name: "StarshinePermissionGrants");

            migrationBuilder.DropTable(
                name: "StarshinePermissionGroups");

            migrationBuilder.DropTable(
                name: "StarshinePermissions");

            migrationBuilder.DropTable(
                name: "StarshineTenantConnectionStrings");

            migrationBuilder.DropTable(
                name: "IdentityServerApiResource");

            migrationBuilder.DropTable(
                name: "IdentityServerApiScope");

            migrationBuilder.DropTable(
                name: "IdentityServerClient");

            migrationBuilder.DropTable(
                name: "IdentityServerIdentityResource");

            migrationBuilder.DropTable(
                name: "StarshineIdentityRole");

            migrationBuilder.DropTable(
                name: "StarshineIdentityUser");

            migrationBuilder.DropTable(
                name: "StarshineOrganizationUnit");

            migrationBuilder.DropTable(
                name: "StarshineTenants");
        }
    }
}
