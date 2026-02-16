# Contributing to Rclone GUI

Thank you for your interest in contributing to Rclone GUI! This document provides guidelines for contributing to the project.

## Code of Conduct

- Be respectful and inclusive
- Welcome newcomers
- Focus on constructive feedback
- Maintain a harassment-free environment

## How to Contribute

### Reporting Bugs

1. **Search Existing Issues**: Check if the bug has already been reported
2. **Create Detailed Report**: Include:
   - OS and version
   - .NET version
   - Rclone version
   - Steps to reproduce
   - Expected behavior
   - Actual behavior
   - Screenshots if applicable
3. **Use Issue Template**: Follow the bug report template if available

### Suggesting Enhancements

1. **Check Existing Suggestions**: Search for similar feature requests
2. **Describe the Feature**: Include:
   - Use case and motivation
   - Proposed solution
   - Alternative solutions considered
   - Impact on existing functionality
3. **Be Open to Discussion**: Maintainers may suggest alternatives

### Pull Requests

1. **Fork the Repository**: Create your own fork
2. **Create a Branch**: Use descriptive branch names
   - `feature/add-new-provider`
   - `fix/oauth-timeout`
   - `docs/update-readme`
3. **Make Changes**: Follow coding standards
4. **Test Thoroughly**: Ensure all changes work as expected
5. **Update Documentation**: Update relevant docs
6. **Commit Messages**: Write clear, descriptive commit messages
7. **Submit PR**: Create a pull request with description of changes

## Development Setup

### Prerequisites

- .NET 8.0 SDK or later
- Git
- IDE (Visual Studio, Rider, or VS Code)
- Rclone (for testing)

### Getting Started

```bash
# Clone your fork
git clone https://github.com/YOUR_USERNAME/Rclone_Gui_IA.git
cd Rclone_Gui_IA

# Build the project
dotnet build

# Run the application
dotnet run --project src/RcloneGui/RcloneGui.csproj
```

## Coding Standards

### C# Style

- Follow standard C# naming conventions
- Use `PascalCase` for public members
- Use `camelCase` for private members
- Use `_camelCase` for private fields
- Use meaningful variable and method names

### XAML Style

- Use proper indentation (2 or 4 spaces)
- Group related properties
- Use meaningful names for UI elements
- Add comments for complex layouts

### Code Organization

- Keep files focused and small
- One class per file (generally)
- Group related functionality
- Use regions sparingly

### Comments

- Add XML documentation for public APIs
- Comment complex logic
- Avoid obvious comments
- Keep comments up to date

## Project Structure

```
src/
├── RcloneGui/           # Main UI application
│   ├── ViewModels/      # MVVM ViewModels
│   ├── Views/           # XAML Views
│   └── Assets/          # Resources and assets
└── RcloneGui.Core/      # Core business logic
    ├── Models/          # Data models
    └── Services/        # Business services
```

## Adding New Features

### Adding a New Provider

1. Update `ProviderService.cs` in `RcloneGui.Core/Services/`
2. Add new `ProviderInfo` to the list
3. Specify authentication type and required fields
4. Test with Rclone CLI first
5. Update documentation

Example:
```csharp
new ProviderInfo
{
    Name = "New Provider",
    Type = "provider_type",
    Description = "Description of provider",
    AuthType = AuthenticationType.OAuth2,
    RequiresBrowser = true,
    RequiredFields = new List<string>()
}
```

### Adding a New View

1. Create XAML file in `Views/`
2. Create code-behind `.cs` file
3. Create ViewModel in `ViewModels/`
4. Add navigation in `MainWindowViewModel`
5. Update `MainWindow.axaml` DataTemplates
6. Test thoroughly

## Testing

### Manual Testing

- Test on target platforms (Windows, Linux, macOS if possible)
- Test all provider types
- Test error conditions
- Test edge cases

### Testing Checklist

- [ ] Application builds without warnings
- [ ] Application runs without errors
- [ ] UI is responsive
- [ ] All features work as expected
- [ ] Error handling works properly
- [ ] Changes don't break existing functionality

## Documentation

### What to Document

- New features
- Changed behavior
- Breaking changes
- Configuration options
- API changes

### Where to Document

- Update README.md for user-facing changes
- Update ARCHITECTURE.md for technical changes
- Update QUICKSTART.md for setup changes
- Add inline comments for complex code
- Update CHANGELOG.md

## Version Control

### Commit Messages

Format:
```
<type>: <subject>

<body>

<footer>
```

Types:
- `feat`: New feature
- `fix`: Bug fix
- `docs`: Documentation changes
- `style`: Code style changes (formatting)
- `refactor`: Code refactoring
- `test`: Adding tests
- `chore`: Maintenance tasks

Example:
```
feat: Add support for Backblaze B2

Implement Backblaze B2 cloud storage provider with API key authentication.
Includes UI updates and provider configuration.

Closes #123
```

### Branching

- `main`: Stable release branch
- `develop`: Development branch
- `feature/*`: Feature branches
- `fix/*`: Bug fix branches
- `docs/*`: Documentation branches

## Release Process

1. Update version number
2. Update CHANGELOG.md
3. Create release branch
4. Final testing
5. Merge to main
6. Tag release
7. Build release binaries
8. Create GitHub release
9. Update documentation

## Getting Help

- **Documentation**: Read existing docs first
- **Issues**: Search existing issues
- **Discussions**: Use GitHub Discussions for questions
- **Email**: Contact maintainers for private matters

## Recognition

Contributors will be recognized in:
- CHANGELOG.md
- GitHub contributors list
- Release notes

## License

By contributing, you agree that your contributions will be licensed under the project's license.

## Thank You!

Your contributions make this project better for everyone. Thank you for taking the time to contribute!
