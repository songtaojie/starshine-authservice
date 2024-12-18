using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Starshine.Authservice.EntityFrameworkCore.Migrations
{
    /// <inheritdoc />
    public partial class InitTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AbpClaimTypes",
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
                    table.PrimaryKey("pk_abp_claim_types", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "AbpLinkUsers",
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
                    table.PrimaryKey("pk_abp_link_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "AbpOrganizationUnits",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false),
                    TenantId = table.Column<Guid>(type: "TEXT", nullable: true),
                    parent_id = table.Column<Guid>(type: "TEXT", nullable: true),
                    Code = table.Column<string>(type: "TEXT", maxLength: 95, nullable: false),
                    DisplayName = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
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
                    table.PrimaryKey("pk_abp_organization_units", x => x.id);
                    table.ForeignKey(
                        name: "fk_abp_organization_units_abp_organization_units_parent_id",
                        column: x => x.parent_id,
                        principalTable: "AbpOrganizationUnits",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "AbpPermissionGrants",
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
                    table.PrimaryKey("pk_abp_permission_grants", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "AbpPermissionGroups",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false),
                    name = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    display_name = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false),
                    ExtraProperties = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_abp_permission_groups", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "AbpPermissions",
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
                    ExtraProperties = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_abp_permissions", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "AbpRoles",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false),
                    TenantId = table.Column<Guid>(type: "TEXT", nullable: true),
                    name = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false),
                    normalized_name = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false),
                    IsDefault = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsStatic = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsPublic = table.Column<bool>(type: "INTEGER", nullable: false),
                    entity_version = table.Column<int>(type: "INTEGER", nullable: false),
                    ExtraProperties = table.Column<string>(type: "TEXT", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_abp_roles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "AbpSecurityLogs",
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
                    client_id = table.Column<string>(type: "TEXT", maxLength: 64, nullable: true),
                    correlation_id = table.Column<string>(type: "TEXT", maxLength: 64, nullable: true),
                    client_ip_address = table.Column<string>(type: "TEXT", maxLength: 64, nullable: true),
                    browser_info = table.Column<string>(type: "TEXT", maxLength: 512, nullable: true),
                    creation_time = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ExtraProperties = table.Column<string>(type: "TEXT", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_abp_security_logs", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "AbpSessions",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false),
                    session_id = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    device = table.Column<string>(type: "TEXT", maxLength: 64, nullable: false),
                    device_info = table.Column<string>(type: "TEXT", maxLength: 64, nullable: true),
                    TenantId = table.Column<Guid>(type: "TEXT", nullable: true),
                    user_id = table.Column<Guid>(type: "TEXT", nullable: false),
                    client_id = table.Column<string>(type: "TEXT", maxLength: 64, nullable: true),
                    ip_addresses = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    signed_in = table.Column<DateTime>(type: "TEXT", nullable: false),
                    last_accessed = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_abp_sessions", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "AbpTenants",
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
                    table.PrimaryKey("pk_abp_tenants", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "AbpUserDelegations",
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
                    table.PrimaryKey("pk_abp_user_delegations", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "AbpUsers",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false),
                    TenantId = table.Column<Guid>(type: "TEXT", nullable: true),
                    UserName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false),
                    NormalizedUserName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 64, nullable: true),
                    Surname = table.Column<string>(type: "TEXT", maxLength: 64, nullable: true),
                    Email = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false),
                    NormalizedEmail = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false),
                    EmailConfirmed = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                    PasswordHash = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    SecurityStamp = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false),
                    IsExternal = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                    PhoneNumber = table.Column<string>(type: "TEXT", maxLength: 16, nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                    lockout_end = table.Column<DateTimeOffset>(type: "TEXT", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                    AccessFailedCount = table.Column<int>(type: "INTEGER", nullable: false, defaultValue: 0),
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
                    table.PrimaryKey("pk_abp_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "AuthApiResources",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false),
                    name = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    display_name = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    description = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: false),
                    enabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    allowed_access_token_signing_algorithms = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
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
                    table.PrimaryKey("pk_auth_api_resources", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "AuthApiScopes",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false),
                    enabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    name = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    display_name = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    description = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: false),
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
                    table.PrimaryKey("pk_auth_api_scopes", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "AuthClients",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false),
                    client_id = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    client_name = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    description = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: false),
                    client_uri = table.Column<string>(type: "TEXT", maxLength: 2000, nullable: false),
                    logo_uri = table.Column<string>(type: "TEXT", maxLength: 2000, nullable: false),
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
                    front_channel_logout_uri = table.Column<string>(type: "TEXT", maxLength: 2000, nullable: false),
                    front_channel_logout_session_required = table.Column<bool>(type: "INTEGER", nullable: false),
                    back_channel_logout_uri = table.Column<string>(type: "TEXT", maxLength: 2000, nullable: false),
                    back_channel_logout_session_required = table.Column<bool>(type: "INTEGER", nullable: false),
                    allow_offline_access = table.Column<bool>(type: "INTEGER", nullable: false),
                    identity_token_lifetime = table.Column<int>(type: "INTEGER", nullable: false),
                    allowed_identity_token_signing_algorithms = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
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
                    client_claims_prefix = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    pair_wise_subject_salt = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    user_sso_lifetime = table.Column<int>(type: "INTEGER", nullable: true),
                    user_code_type = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
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
                    table.PrimaryKey("pk_auth_clients", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "AuthDeviceFlowCodes",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false),
                    device_code = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    user_code = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    subject_id = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    session_id = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    client_id = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    description = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    expiration = table.Column<DateTime>(type: "TEXT", nullable: false),
                    data = table.Column<string>(type: "TEXT", maxLength: 50000, nullable: false),
                    ExtraProperties = table.Column<string>(type: "TEXT", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatorId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_auth_device_flow_codes", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "AuthIdentityResources",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false),
                    name = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    display_name = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    description = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: false),
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
                    table.PrimaryKey("pk_auth_identity_resources", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "AuthPersistedGrants",
                columns: table => new
                {
                    key = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    type = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    subject_id = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    session_id = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    client_id = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    description = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    creation_time = table.Column<DateTime>(type: "TEXT", nullable: false),
                    expiration = table.Column<DateTime>(type: "TEXT", nullable: true),
                    consumed_time = table.Column<DateTime>(type: "TEXT", nullable: true),
                    data = table.Column<string>(type: "TEXT", maxLength: 50000, nullable: false),
                    id = table.Column<Guid>(type: "TEXT", nullable: false),
                    ExtraProperties = table.Column<string>(type: "TEXT", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_auth_persisted_grants", x => x.key);
                });

            migrationBuilder.CreateTable(
                name: "AbpOrganizationUnitRoles",
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
                    table.PrimaryKey("pk_abp_organization_unit_roles", x => new { x.organization_unit_id, x.role_id });
                    table.ForeignKey(
                        name: "fk_abp_organization_unit_roles_abp_organization_units_organization_unit_id",
                        column: x => x.organization_unit_id,
                        principalTable: "AbpOrganizationUnits",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_abp_organization_unit_roles_abp_roles_role_id",
                        column: x => x.role_id,
                        principalTable: "AbpRoles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AbpRoleClaims",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false),
                    role_id = table.Column<Guid>(type: "TEXT", nullable: false),
                    TenantId = table.Column<Guid>(type: "TEXT", nullable: true),
                    claim_type = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false),
                    claim_value = table.Column<string>(type: "TEXT", maxLength: 1024, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_abp_role_claims", x => x.id);
                    table.ForeignKey(
                        name: "fk_abp_role_claims_abp_roles_role_id",
                        column: x => x.role_id,
                        principalTable: "AbpRoles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AbpTenantConnectionStrings",
                columns: table => new
                {
                    tenant_id = table.Column<Guid>(type: "TEXT", nullable: false),
                    name = table.Column<string>(type: "TEXT", maxLength: 64, nullable: false),
                    value = table.Column<string>(type: "TEXT", maxLength: 1024, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_abp_tenant_connection_strings", x => new { x.tenant_id, x.name });
                    table.ForeignKey(
                        name: "fk_abp_tenant_connection_strings_abp_tenants_tenant_id",
                        column: x => x.tenant_id,
                        principalTable: "AbpTenants",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AbpUserClaims",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false),
                    user_id = table.Column<Guid>(type: "TEXT", nullable: false),
                    TenantId = table.Column<Guid>(type: "TEXT", nullable: true),
                    claim_type = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false),
                    claim_value = table.Column<string>(type: "TEXT", maxLength: 1024, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_abp_user_claims", x => x.id);
                    table.ForeignKey(
                        name: "fk_abp_user_claims_abp_users_user_id",
                        column: x => x.user_id,
                        principalTable: "AbpUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AbpUserLogins",
                columns: table => new
                {
                    user_id = table.Column<Guid>(type: "TEXT", nullable: false),
                    login_provider = table.Column<string>(type: "TEXT", maxLength: 64, nullable: false),
                    TenantId = table.Column<Guid>(type: "TEXT", nullable: true),
                    provider_key = table.Column<string>(type: "TEXT", maxLength: 196, nullable: false),
                    provider_display_name = table.Column<string>(type: "TEXT", maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_abp_user_logins", x => new { x.user_id, x.login_provider });
                    table.ForeignKey(
                        name: "fk_abp_user_logins_abp_users_user_id",
                        column: x => x.user_id,
                        principalTable: "AbpUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AbpUserOrganizationUnits",
                columns: table => new
                {
                    user_id = table.Column<Guid>(type: "TEXT", nullable: false),
                    organization_unit_id = table.Column<Guid>(type: "TEXT", nullable: false),
                    TenantId = table.Column<Guid>(type: "TEXT", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatorId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_abp_user_organization_units", x => new { x.organization_unit_id, x.user_id });
                    table.ForeignKey(
                        name: "fk_abp_user_organization_units_abp_organization_units_organization_unit_id",
                        column: x => x.organization_unit_id,
                        principalTable: "AbpOrganizationUnits",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_abp_user_organization_units_abp_users_user_id",
                        column: x => x.user_id,
                        principalTable: "AbpUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AbpUserRoles",
                columns: table => new
                {
                    user_id = table.Column<Guid>(type: "TEXT", nullable: false),
                    role_id = table.Column<Guid>(type: "TEXT", nullable: false),
                    TenantId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_abp_user_roles", x => new { x.user_id, x.role_id });
                    table.ForeignKey(
                        name: "fk_abp_user_roles_abp_users_user_id",
                        column: x => x.user_id,
                        principalTable: "AbpUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_abp_user_roles_roles_role_id",
                        column: x => x.role_id,
                        principalTable: "AbpRoles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AbpUserTokens",
                columns: table => new
                {
                    user_id = table.Column<Guid>(type: "TEXT", nullable: false),
                    login_provider = table.Column<string>(type: "TEXT", maxLength: 64, nullable: false),
                    name = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    TenantId = table.Column<Guid>(type: "TEXT", nullable: true),
                    value = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_abp_user_tokens", x => new { x.user_id, x.login_provider, x.name });
                    table.ForeignKey(
                        name: "fk_abp_user_tokens_abp_users_user_id",
                        column: x => x.user_id,
                        principalTable: "AbpUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AuthApiResourceClaims",
                columns: table => new
                {
                    type = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    api_resource_id = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_auth_api_resource_claims", x => new { x.api_resource_id, x.type });
                    table.ForeignKey(
                        name: "fk_auth_api_resource_claims_auth_api_resources_api_resource_id",
                        column: x => x.api_resource_id,
                        principalTable: "AuthApiResources",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AuthApiResourceProperties",
                columns: table => new
                {
                    api_resource_id = table.Column<Guid>(type: "TEXT", nullable: false),
                    key = table.Column<string>(type: "TEXT", maxLength: 250, nullable: false),
                    value = table.Column<string>(type: "TEXT", maxLength: 2000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_auth_api_resource_properties", x => new { x.api_resource_id, x.key, x.value });
                    table.ForeignKey(
                        name: "fk_auth_api_resource_properties_auth_api_resources_api_resource_id",
                        column: x => x.api_resource_id,
                        principalTable: "AuthApiResources",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AuthApiResourceScopes",
                columns: table => new
                {
                    api_resource_id = table.Column<Guid>(type: "TEXT", nullable: false),
                    scope = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_auth_api_resource_scopes", x => new { x.api_resource_id, x.scope });
                    table.ForeignKey(
                        name: "fk_auth_api_resource_scopes_auth_api_resources_api_resource_id",
                        column: x => x.api_resource_id,
                        principalTable: "AuthApiResources",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AuthApiResourceSecrets",
                columns: table => new
                {
                    type = table.Column<string>(type: "TEXT", maxLength: 250, nullable: false),
                    value = table.Column<string>(type: "TEXT", maxLength: 4000, nullable: false),
                    api_resource_id = table.Column<Guid>(type: "TEXT", nullable: false),
                    description = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: false),
                    expiration = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_auth_api_resource_secrets", x => new { x.api_resource_id, x.type, x.value });
                    table.ForeignKey(
                        name: "fk_auth_api_resource_secrets_auth_api_resources_api_resource_id",
                        column: x => x.api_resource_id,
                        principalTable: "AuthApiResources",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AuthApiScopeClaims",
                columns: table => new
                {
                    type = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    api_scope_id = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_auth_api_scope_claims", x => new { x.api_scope_id, x.type });
                    table.ForeignKey(
                        name: "fk_auth_api_scope_claims_auth_api_scopes_api_scope_id",
                        column: x => x.api_scope_id,
                        principalTable: "AuthApiScopes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AuthApiScopeProperties",
                columns: table => new
                {
                    api_scope_id = table.Column<Guid>(type: "TEXT", nullable: false),
                    key = table.Column<string>(type: "TEXT", maxLength: 250, nullable: false),
                    value = table.Column<string>(type: "TEXT", maxLength: 2000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_auth_api_scope_properties", x => new { x.api_scope_id, x.key, x.value });
                    table.ForeignKey(
                        name: "fk_auth_api_scope_properties_auth_api_scopes_api_scope_id",
                        column: x => x.api_scope_id,
                        principalTable: "AuthApiScopes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AuthClientClaims",
                columns: table => new
                {
                    client_id = table.Column<Guid>(type: "TEXT", nullable: false),
                    type = table.Column<string>(type: "TEXT", maxLength: 250, nullable: false),
                    value = table.Column<string>(type: "TEXT", maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_auth_client_claims", x => new { x.client_id, x.type, x.value });
                    table.ForeignKey(
                        name: "fk_auth_client_claims_auth_clients_client_id",
                        column: x => x.client_id,
                        principalTable: "AuthClients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AuthClientCorsOrigins",
                columns: table => new
                {
                    client_id = table.Column<Guid>(type: "TEXT", nullable: false),
                    origin = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_auth_client_cors_origins", x => new { x.client_id, x.origin });
                    table.ForeignKey(
                        name: "fk_auth_client_cors_origins_auth_clients_client_id",
                        column: x => x.client_id,
                        principalTable: "AuthClients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AuthClientGrantTypes",
                columns: table => new
                {
                    client_id = table.Column<Guid>(type: "TEXT", nullable: false),
                    grant_type = table.Column<string>(type: "TEXT", maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_auth_client_grant_types", x => new { x.client_id, x.grant_type });
                    table.ForeignKey(
                        name: "fk_auth_client_grant_types_auth_clients_client_id",
                        column: x => x.client_id,
                        principalTable: "AuthClients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AuthClientIdPRestrictions",
                columns: table => new
                {
                    client_id = table.Column<Guid>(type: "TEXT", nullable: false),
                    provider = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_auth_client_id_p_restrictions", x => new { x.client_id, x.provider });
                    table.ForeignKey(
                        name: "fk_auth_client_id_p_restrictions_auth_clients_client_id",
                        column: x => x.client_id,
                        principalTable: "AuthClients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AuthClientPostLogoutRedirectUris",
                columns: table => new
                {
                    client_id = table.Column<Guid>(type: "TEXT", nullable: false),
                    post_logout_redirect_uri = table.Column<string>(type: "TEXT", maxLength: 2000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_auth_client_post_logout_redirect_uris", x => new { x.client_id, x.post_logout_redirect_uri });
                    table.ForeignKey(
                        name: "fk_auth_client_post_logout_redirect_uris_auth_clients_client_id",
                        column: x => x.client_id,
                        principalTable: "AuthClients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AuthClientProperties",
                columns: table => new
                {
                    client_id = table.Column<Guid>(type: "TEXT", nullable: false),
                    key = table.Column<string>(type: "TEXT", maxLength: 250, nullable: false),
                    value = table.Column<string>(type: "TEXT", maxLength: 2000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_auth_client_properties", x => new { x.client_id, x.key, x.value });
                    table.ForeignKey(
                        name: "fk_auth_client_properties_auth_clients_client_id",
                        column: x => x.client_id,
                        principalTable: "AuthClients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AuthClientRedirectUris",
                columns: table => new
                {
                    client_id = table.Column<Guid>(type: "TEXT", nullable: false),
                    redirect_uri = table.Column<string>(type: "TEXT", maxLength: 2000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_auth_client_redirect_uris", x => new { x.client_id, x.redirect_uri });
                    table.ForeignKey(
                        name: "fk_auth_client_redirect_uris_auth_clients_client_id",
                        column: x => x.client_id,
                        principalTable: "AuthClients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AuthClientScopes",
                columns: table => new
                {
                    client_id = table.Column<Guid>(type: "TEXT", nullable: false),
                    scope = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_auth_client_scopes", x => new { x.client_id, x.scope });
                    table.ForeignKey(
                        name: "fk_auth_client_scopes_auth_clients_client_id",
                        column: x => x.client_id,
                        principalTable: "AuthClients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AuthClientSecrets",
                columns: table => new
                {
                    type = table.Column<string>(type: "TEXT", maxLength: 250, nullable: false),
                    value = table.Column<string>(type: "TEXT", maxLength: 4000, nullable: false),
                    client_id = table.Column<Guid>(type: "TEXT", nullable: false),
                    description = table.Column<string>(type: "TEXT", maxLength: 2000, nullable: false),
                    expiration = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_auth_client_secrets", x => new { x.client_id, x.type, x.value });
                    table.ForeignKey(
                        name: "fk_auth_client_secrets_auth_clients_client_id",
                        column: x => x.client_id,
                        principalTable: "AuthClients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AuthIdentityResourceClaims",
                columns: table => new
                {
                    type = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    identity_resource_id = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_auth_identity_resource_claims", x => new { x.identity_resource_id, x.type });
                    table.ForeignKey(
                        name: "fk_auth_identity_resource_claims_auth_identity_resources_identity_resource_id",
                        column: x => x.identity_resource_id,
                        principalTable: "AuthIdentityResources",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AuthIdentityResourceProperties",
                columns: table => new
                {
                    identity_resource_id = table.Column<Guid>(type: "TEXT", nullable: false),
                    key = table.Column<string>(type: "TEXT", maxLength: 250, nullable: false),
                    value = table.Column<string>(type: "TEXT", maxLength: 2000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_auth_identity_resource_properties", x => new { x.identity_resource_id, x.key, x.value });
                    table.ForeignKey(
                        name: "fk_auth_identity_resource_properties_auth_identity_resources_identity_resource_id",
                        column: x => x.identity_resource_id,
                        principalTable: "AuthIdentityResources",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_abp_link_users_source_user_id_source_tenant_id_target_user_id_target_tenant_id",
                table: "AbpLinkUsers",
                columns: new[] { "source_user_id", "source_tenant_id", "target_user_id", "target_tenant_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_abp_organization_unit_roles_role_id_organization_unit_id",
                table: "AbpOrganizationUnitRoles",
                columns: new[] { "role_id", "organization_unit_id" });

            migrationBuilder.CreateIndex(
                name: "ix_abp_organization_units_code",
                table: "AbpOrganizationUnits",
                column: "Code");

            migrationBuilder.CreateIndex(
                name: "ix_abp_organization_units_parent_id",
                table: "AbpOrganizationUnits",
                column: "parent_id");

            migrationBuilder.CreateIndex(
                name: "ix_abp_permission_grants_tenant_id_name_provider_name_provider_key",
                table: "AbpPermissionGrants",
                columns: new[] { "TenantId", "name", "provider_name", "provider_key" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_abp_permission_groups_name",
                table: "AbpPermissionGroups",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_abp_permissions_group_name",
                table: "AbpPermissions",
                column: "group_name");

            migrationBuilder.CreateIndex(
                name: "ix_abp_permissions_name",
                table: "AbpPermissions",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_abp_role_claims_role_id",
                table: "AbpRoleClaims",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "ix_abp_roles_normalized_name",
                table: "AbpRoles",
                column: "normalized_name");

            migrationBuilder.CreateIndex(
                name: "ix_abp_security_logs_tenant_id_action",
                table: "AbpSecurityLogs",
                columns: new[] { "TenantId", "action" });

            migrationBuilder.CreateIndex(
                name: "ix_abp_security_logs_tenant_id_application_name",
                table: "AbpSecurityLogs",
                columns: new[] { "TenantId", "application_name" });

            migrationBuilder.CreateIndex(
                name: "ix_abp_security_logs_tenant_id_identity",
                table: "AbpSecurityLogs",
                columns: new[] { "TenantId", "identity" });

            migrationBuilder.CreateIndex(
                name: "ix_abp_security_logs_tenant_id_user_id",
                table: "AbpSecurityLogs",
                columns: new[] { "TenantId", "user_id" });

            migrationBuilder.CreateIndex(
                name: "ix_abp_sessions_device",
                table: "AbpSessions",
                column: "device");

            migrationBuilder.CreateIndex(
                name: "ix_abp_sessions_session_id",
                table: "AbpSessions",
                column: "session_id");

            migrationBuilder.CreateIndex(
                name: "ix_abp_sessions_tenant_id_user_id",
                table: "AbpSessions",
                columns: new[] { "TenantId", "user_id" });

            migrationBuilder.CreateIndex(
                name: "ix_abp_tenants_name",
                table: "AbpTenants",
                column: "name");

            migrationBuilder.CreateIndex(
                name: "ix_abp_tenants_normalized_name",
                table: "AbpTenants",
                column: "normalized_name");

            migrationBuilder.CreateIndex(
                name: "ix_abp_user_claims_user_id",
                table: "AbpUserClaims",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_abp_user_logins_login_provider_provider_key",
                table: "AbpUserLogins",
                columns: new[] { "login_provider", "provider_key" });

            migrationBuilder.CreateIndex(
                name: "ix_abp_user_organization_units_user_id_organization_unit_id",
                table: "AbpUserOrganizationUnits",
                columns: new[] { "user_id", "organization_unit_id" });

            migrationBuilder.CreateIndex(
                name: "ix_abp_user_roles_role_id_user_id",
                table: "AbpUserRoles",
                columns: new[] { "role_id", "user_id" });

            migrationBuilder.CreateIndex(
                name: "ix_abp_users_email",
                table: "AbpUsers",
                column: "Email");

            migrationBuilder.CreateIndex(
                name: "ix_abp_users_normalized_email",
                table: "AbpUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "ix_abp_users_normalized_user_name",
                table: "AbpUsers",
                column: "NormalizedUserName");

            migrationBuilder.CreateIndex(
                name: "ix_abp_users_user_name",
                table: "AbpUsers",
                column: "UserName");

            migrationBuilder.CreateIndex(
                name: "ix_auth_clients_client_id",
                table: "AuthClients",
                column: "client_id");

            migrationBuilder.CreateIndex(
                name: "ix_auth_device_flow_codes_device_code",
                table: "AuthDeviceFlowCodes",
                column: "device_code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_auth_device_flow_codes_expiration",
                table: "AuthDeviceFlowCodes",
                column: "expiration");

            migrationBuilder.CreateIndex(
                name: "ix_auth_device_flow_codes_user_code",
                table: "AuthDeviceFlowCodes",
                column: "user_code");

            migrationBuilder.CreateIndex(
                name: "ix_auth_persisted_grants_expiration",
                table: "AuthPersistedGrants",
                column: "expiration");

            migrationBuilder.CreateIndex(
                name: "ix_auth_persisted_grants_subject_id_client_id_type",
                table: "AuthPersistedGrants",
                columns: new[] { "subject_id", "client_id", "type" });

            migrationBuilder.CreateIndex(
                name: "ix_auth_persisted_grants_subject_id_session_id_type",
                table: "AuthPersistedGrants",
                columns: new[] { "subject_id", "session_id", "type" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AbpClaimTypes");

            migrationBuilder.DropTable(
                name: "AbpLinkUsers");

            migrationBuilder.DropTable(
                name: "AbpOrganizationUnitRoles");

            migrationBuilder.DropTable(
                name: "AbpPermissionGrants");

            migrationBuilder.DropTable(
                name: "AbpPermissionGroups");

            migrationBuilder.DropTable(
                name: "AbpPermissions");

            migrationBuilder.DropTable(
                name: "AbpRoleClaims");

            migrationBuilder.DropTable(
                name: "AbpSecurityLogs");

            migrationBuilder.DropTable(
                name: "AbpSessions");

            migrationBuilder.DropTable(
                name: "AbpTenantConnectionStrings");

            migrationBuilder.DropTable(
                name: "AbpUserClaims");

            migrationBuilder.DropTable(
                name: "AbpUserDelegations");

            migrationBuilder.DropTable(
                name: "AbpUserLogins");

            migrationBuilder.DropTable(
                name: "AbpUserOrganizationUnits");

            migrationBuilder.DropTable(
                name: "AbpUserRoles");

            migrationBuilder.DropTable(
                name: "AbpUserTokens");

            migrationBuilder.DropTable(
                name: "AuthApiResourceClaims");

            migrationBuilder.DropTable(
                name: "AuthApiResourceProperties");

            migrationBuilder.DropTable(
                name: "AuthApiResourceScopes");

            migrationBuilder.DropTable(
                name: "AuthApiResourceSecrets");

            migrationBuilder.DropTable(
                name: "AuthApiScopeClaims");

            migrationBuilder.DropTable(
                name: "AuthApiScopeProperties");

            migrationBuilder.DropTable(
                name: "AuthClientClaims");

            migrationBuilder.DropTable(
                name: "AuthClientCorsOrigins");

            migrationBuilder.DropTable(
                name: "AuthClientGrantTypes");

            migrationBuilder.DropTable(
                name: "AuthClientIdPRestrictions");

            migrationBuilder.DropTable(
                name: "AuthClientPostLogoutRedirectUris");

            migrationBuilder.DropTable(
                name: "AuthClientProperties");

            migrationBuilder.DropTable(
                name: "AuthClientRedirectUris");

            migrationBuilder.DropTable(
                name: "AuthClientScopes");

            migrationBuilder.DropTable(
                name: "AuthClientSecrets");

            migrationBuilder.DropTable(
                name: "AuthDeviceFlowCodes");

            migrationBuilder.DropTable(
                name: "AuthIdentityResourceClaims");

            migrationBuilder.DropTable(
                name: "AuthIdentityResourceProperties");

            migrationBuilder.DropTable(
                name: "AuthPersistedGrants");

            migrationBuilder.DropTable(
                name: "AbpTenants");

            migrationBuilder.DropTable(
                name: "AbpOrganizationUnits");

            migrationBuilder.DropTable(
                name: "AbpRoles");

            migrationBuilder.DropTable(
                name: "AbpUsers");

            migrationBuilder.DropTable(
                name: "AuthApiResources");

            migrationBuilder.DropTable(
                name: "AuthApiScopes");

            migrationBuilder.DropTable(
                name: "AuthClients");

            migrationBuilder.DropTable(
                name: "AuthIdentityResources");
        }
    }
}
