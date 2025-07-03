# Identity Service API

A comprehensive authentication and identity management service built with ASP.NET Core, providing secure user registration, login, OTP verification, and password management capabilities.

## 🚀 Features

- **User Registration** - Create new user accounts with validation
- **User Authentication** - Secure login with email and password
- **OTP Verification** - Two-factor authentication via email OTP
- **Token Management** - JWT access and refresh token handling
- **Password Reset** - Secure password reset via email verification
- **IP Tracking** - Client IP address logging for security
- **Session Management** - User logout and session invalidation

## 📋 API Endpoints

### Authentication Endpoints

| Method | Endpoint | Description | Request Body |
|--------|----------|-------------|--------------|
| `POST` | `/api/auth/register` | Register a new user | `UserRegisterDto` |
| `POST` | `/api/auth/login` | Login with email/password | `UserLoginDto` |
| `POST` | `/api/auth/validate-otp` | Verify OTP code | `RequestValidateOtpDto` |
| `GET` | `/api/auth/refresh-token` | Refresh access token | Query: `email` |
| `GET` | `/api/auth/logout` | Logout user | Query: `email` |

### Password Management Endpoints

| Method | Endpoint | Description | Request Body |
|--------|----------|-------------|--------------|
| `POST` | `/api/auth/forgot-password` | Request password reset | `string email` |
| `GET` | `/api/auth/validate-resetToken` | Validate reset token | Query: `id`, `resetToken` |
| `POST` | `/api/auth/reset-password` | Reset user password | `RequestResetPasswordDto` |

## 📝 Request/Response Models

### Registration Request
```json
{
  "email": "user@example.com",
  "password": "SecurePassword123!",
  "firstName": "John",
  "lastName": "Doe",
  "username": "johndoe",
  "dateOfBirth": "1990-01-01",
  "gender": 0
}
```

### Login Request
```json
{
  "email": "user@example.com",
  "password": "SecurePassword123!"
}
```

### OTP Validation Request
```json
{
  "email": "user@example.com",
  "otp": "123456"
}
```

### Password Reset Request
```json
{
  "email": "user@example.com",
  "newPassword": "NewSecurePassword123!"
}
```

### Token Response
```json
{
  "accessToken": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "refreshToken": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "expiresIn": 3600,
  "tokenType": "Bearer"
}
```

## 🔐 Security Features

- **IP Address Tracking** - Logs client IP addresses for security monitoring
- **Proxy Support** - Handles `X-Forwarded-For` header for requests behind proxies
- **OTP Verification** - Two-factor authentication for enhanced security
- **Token-based Authentication** - JWT tokens for stateless authentication
- **Secure Password Reset** - Email-based password reset with verification tokens

## 🛠️ Setup & Installation

### Prerequisites
- .NET 8.0 or later
- SQL Server or compatible database
- Email service configuration (SMTP)

### Installation Steps

1. **Clone the repository**
   ```bash
   git clone <repository-url>
   cd IdentityService
   ```

2. **Configure Database**
   ```bash
   dotnet ef database update
   ```

3. **Configure Email Settings**
   Update `appsettings.json` with your email service configuration:
   ```json
   {
     "EmailSettings": {
       "SmtpServer": "smtp.gmail.com",
       "SmtpPort": 587,
       "FromEmail": "your-email@gmail.com",
       "FromPassword": "your-app-password"
     }
   }
   ```

4. **Run the Application**
   ```bash
   dotnet run
   ```

## 🔧 Configuration

### JWT Settings
```json
{
  "JwtSettings": {
    "SecretKey": "your-secret-key",
    "Issuer": "IdentityService",
    "Audience": "IdentityService-Client",
    "ExpiryMinutes": 60,
    "RefreshTokenExpiryDays": 7
  }
}
```

### Database Connection
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=IdentityServiceDb;Trusted_Connection=true;"
  }
}
```

## 🚀 Usage Examples

### Register a New User
```bash
curl -X POST "https://localhost:7022/api/auth/register" \
  -H "Content-Type: application/json" \
  -d '{
    "email": "user@example.com",
    "password": "SecurePassword123!",
    "firstName": "John",
    "lastName": "Doe",
    "username": "johndoe",
    "dateOfBirth": "1990-01-01",
    "gender": 0
  }'
```

### Login
```bash
curl -X POST "https://localhost:7022/api/auth/login" \
  -H "Content-Type: application/json" \
  -d '{
    "email": "user@example.com",
    "password": "SecurePassword123!"
  }'
```

### Validate OTP
```bash
curl -X POST "https://localhost:7022/api/auth/validate-otp" \
  -H "Content-Type: application/json" \
  -d '{
    "email": "user@example.com",
    "otp": "123456"
  }'
```

## 🔍 Error Handling

The API returns appropriate HTTP status codes and error messages:

- `200 OK` - Successful operation
- `400 Bad Request` - Invalid input or validation errors
- `401 Unauthorized` - Authentication failed
- `404 Not Found` - Resource not found
- `500 Internal Server Error` - Server error

### Error Response Format
```json
{
  "error": "Error message description",
  "details": "Additional error details if available"
}
```

## 🧪 Testing

### Unit Tests
```bash
dotnet test IdentityService.Tests
```

### Integration Tests
```bash
dotnet test IdentityService.IntegrationTests
```

### API Testing with Postman
Import the provided Postman collection for comprehensive API testing.

## 📊 Monitoring & Logging

The service includes comprehensive logging for:
- Authentication attempts
- OTP generation and validation
- Token operations
- Password reset attempts
- IP address tracking

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## 📄 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 📞 Support

For support and questions:
- Create an issue on GitHub
- Email: support@identityservice.com
- Documentation: [Wiki](https://github.com/your-repo/wiki)

## 🔄 Version History

- **v1.0.0** - Initial release with basic authentication features
- **v1.1.0** - Added OTP verification and password reset
- **v1.2.0** - Enhanced security with IP tracking and improved token management

---
