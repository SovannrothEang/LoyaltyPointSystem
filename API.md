## FR
### Tenant management:

| Action | Input  | Output | Access Level |
|--------|--------|--------|-----------|
| `Create` | Name, MetaData | TenantId | Admin |
| `Update` | route: TenantId, body: Status, Name, Setting | | Admin |
| `Deactivate` | route: TenantId | | Admin |
| `Retrieve` | route: TenantId | Details | Admin |

### Customer management:
- create
- link acc
- retrieve
- validate customer-tenant rs

| Action | Input  | Output | Access Level |
|--------|--------|--------|-----------|
| `Create` | Name, Email, Phone | TenantId | Admin |
| `Link` | route: TenantId, body: Status, Name, Setting | | Admin |
| `Deactivate` | route: TenantId | | Admin |
| `Retrieve` | route: TenantId | Details | Admin |

### Point transaction management:
- points earning
- points redemption
- points adjustment
- transaction history

### Customer dashboard
### Security and access control
### System logging and audit

## Entities
| Name | Field |
|------|-------|
| ``Customer`` | |
| ``Tenant`` | |

tenant:
- tenantId
- name
- status
- settings
loyaltyaccount
