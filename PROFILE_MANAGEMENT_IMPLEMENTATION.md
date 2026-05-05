# File Format Profile Management - Implementation Summary

## Overview
Added a comprehensive profile management system to the Patient Eligibility Translator settings that allows users to create, edit, and manage customized input file format profiles.

## Changes Made

### 1. **FileFormatProfile.cs** (NEW)
Core model class for defining customizable input file formats:
- **Properties:**
  - `Name`: Profile identifier
  - `Description`: Profile description
  - `FieldDelimiter`: Character(s) separating fields (default: ":")
  - `LineDelimiter`: Character(s) separating lines (default: "\n")
  - `PatientDelimiter`: Character(s) separating patient records (default: "---PATIENT---")
  - `FieldMappings`: Dictionary mapping input field names to patient properties
  - `IsDefault`: Boolean flag to protect the default profile

- **Methods:**
  - `CreateDefault()`: Static factory method creating the default profile
  - `ToString()`: User-friendly display format

### 2. **AppSettings.cs** (UPDATED)
Extended application settings to support profile management:
- **New Properties:**
  - `Profiles`: List<FileFormatProfile> - stores all configured profiles
  - `ActiveProfileName`: string - tracks the currently selected profile

- **New Methods:**
  - `GetActiveProfile()`: Returns the active profile or default
  - `AddProfile()`: Adds a new profile to the collection
  - `RemoveProfile()`: Removes a profile (prevents deletion of default)

- **Enhanced Methods:**
  - `Load()`: Now ensures the default profile is always present
  - `Save()`: Now serializes profiles along with other settings

### 3. **ProfileManagementForm.cs** (NEW)
Main UI for managing file format profiles:
- Displays a list of custom profiles (excludes default)
- Shows profile details (name, description, delimiters)
- Buttons for creating new profiles, editing existing ones, and deleting profiles
- Prevents deletion of the default profile

### 4. **ProfileManagementForm.Designer.cs** (NEW)
Designer file with UI layout for ProfileManagementForm

### 5. **ProfileEditForm.cs** (NEW)
Dialog form for creating and editing file format profiles:
- Text fields for name, description, and delimiter values
- Automatic escaping/unescaping of special characters (\n, \t, \r)
- Validation ensuring profile names are provided
- Prevents editing the default profile name

### 6. **ProfileEditForm.Designer.cs** (NEW)
Designer file with UI layout for ProfileEditForm

### 7. **SettingsForm.cs** (UPDATED)
Enhanced settings form to include profile management:
- Added new constructor parameter for profiles and active profile name
- New `LoadProfiles()` method to populate profile dropdown
- New button "Manage..." to open ProfileManagementForm
- Displays available profiles in a combo box for selection
- Saves selected profile as active when OK is clicked

### 8. **SettingsForm.Designer.cs** (UPDATED)
Updated form designer:
- Added ComboBox (`cbProfiles`) for profile selection
- Added Label (`lblProfile`) for profile dropdown
- Added Button (`btnManageProfiles`) to access profile management
- Added GroupBox (`gbProfileSettings`) to organize profile controls
- Increased form height to accommodate new controls

### 9. **Form1.cs** (UPDATED)
Updated main form to use new profile system:
- Modified `BtnSettings_Click()` to pass profiles and active profile name to SettingsForm
- Updates `_appSettings.Profiles` and `_appSettings.ActiveProfileName` based on settings form results

## User Experience Flow

1. **Access Settings**: Click "Settings" button on main form
2. **View Settings Form**: 
   - Existing EDI 270 settings (Single Patient, Max Patients/Batch)
   - NEW: File Format Profiles section with dropdown and "Manage..." button
3. **Manage Profiles**: Click "Manage..." to open ProfileManagementForm
4. **Create New Profile**: Click "New" ? Enter profile details (name, description, delimiters)
5. **Edit Profile**: Select profile ? Click "Edit" ? Modify settings
6. **Delete Profile**: Select profile ? Click "Delete" (cannot delete default)
7. **Save & Apply**: Click OK to save settings including selected profile

## Data Persistence

- All profiles are serialized to JSON in `%APPDATA%\PatientEligibilityTranslator\settings.json`
- Default profile is always included and cannot be removed
- Active profile name is saved with other settings
- Profile data includes field mappings for extensibility

## Future Enhancement Opportunities

1. **Field Mapping UI**: Create UI to customize field mappings per profile
2. **Profile Import/Export**: Allow users to share profile configurations
3. **Format Preview**: Show sample parsing with current profile settings
4. **Validation Rules**: Add field-level validation rules to profiles
5. **Profile Templates**: Pre-built profiles for common formats (CSV, XML, etc.)

## Technical Notes

- Uses JSON serialization with property name attributes for clean config files
- Implements defensive copying to prevent external modification of profile lists
- Provides escape sequence support for special delimiters
- Maintains backward compatibility with existing settings
