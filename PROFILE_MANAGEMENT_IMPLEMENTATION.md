# File Format Profile Management - Implementation Summary

## Overview
Implemented a comprehensive profile management system for the Patient Eligibility Translator that allows users to create, edit, and manage customized input file format profiles with flexible field delimiter configuration and field mapping capabilities.

## Changes Made

### 1. **FileFormatProfile.cs** (NEW)
Core model class for defining customizable input file formats:
- **Properties:**
  - `Name`: Profile identifier (string)
  - `Description`: Profile description (string)
  - `FieldDelimiter`: Character(s) separating fields with defaults: comma, pipe, semicolon, or custom (default: ":")
  - `LineDelimiter`: Character(s) separating lines with presets: LF, CRLF, CR, or custom (default: "\\n")
  - `PatientDelimiter`: Character(s) separating patient records (default: "---PATIENT---")
  - `FieldMappings`: Dictionary<string, string> mapping input field names to patient properties
  - `ColumnIndexMappings`: Dictionary<int, string> for column-based format parsing
  - `UseLineDelimiterForPatients`: Boolean flag for patient separation logic (default: false)
  - `FirstLineIsHeader`: Boolean flag indicating if first line contains column headers (default: false)
  - `FormatType`: Enum supporting KeyValue, Delimited, Csv, FixedWidth formats
  - `IsDefault`: Boolean flag to protect the default profile from modification

- **Methods:**
  - `CreateDefault()`: Static factory method creating the default profile
  - `ToString()`: User-friendly display format

### 2. **ProfileEditForm.cs** (ENHANCED)
Dialog form for creating and editing file format profiles:

**Delimiter Configuration:**
- **Field Delimiter Dropdown** with presets:
  - Common options: comma (,), pipe (|), semicolon (;), tab (\t)
  - Custom option with greyed-out textbox (enabled only when "Custom" is selected)
  - Dynamic show/hide of custom input based on selection
  - SelectedIndexChanged event handler for UI state management

- **Line Delimiter Dropdown** with readable labels:
  - Display labels: "LF (\\n)", "CRLF (\\r\\n)", "CR (\\r)", "Custom"
  - Actual delimiters stored and retrieved correctly
  - Custom option with same dynamic behavior as field delimiter
  - Automatic focus to custom textbox when selected
  - SelectedIndexChanged event handler for UI state management

**First Line Header Checkbox:**
- Boolean toggle for CSV/Delimited files with header row

**Field Mapping Configuration:**
- "Configure Field Mappings..." button launches FieldMapperForm
- Allows add/edit/remove field mappings for flexible input parsing
- Maps input field names to output properties

**Implementation Details:**
- Escape/unescape helper methods for special characters (\n, \t, \r)
- Form state management with greyed-out custom textboxes
- Defensive copying of profile data to prevent external modification
- Validation ensuring profile name is not empty
- Prevention of editing default profile name

### 3. **ProfileEditForm.Designer.cs** (ENHANCED)
Designer file with updated UI layout:
- Field delimiter dropdown (150px) + custom textbox (64px) side-by-side
- Line delimiter dropdown (150px) + custom textbox (64px) side-by-side
- First line header checkbox
- "Configure Field Mappings..." button (220px width)
- Proper tab indices and control grouping
- Form size: 384x275 pixels

### 4. **FieldMapperForm.cs** (NEW)
Dedicated form for managing field input-to-output mappings:

**Features:**
- DataGridView displaying all current mappings with columns:
  - Input Field Name (the key user enters in input files)
  - Map To Property (the patient object property to populate)
  
- **Add Mapping Controls:**
  - Input Field textbox for entering source field names
  - Output Property textbox for entering target property names
  - Add button with validation:
    - Prevents empty entries
    - Prevents duplicate input field mappings
    - Auto-lowercases input field names for case-insensitive matching
    - Clears inputs after successful addition
    - Auto-focuses input field for rapid data entry

- **Remove Mapping:**
  - Remove button to delete selected mapping from DataGridView
  - Validation to ensure a mapping is selected

- **Dialog Result:**
  - OK button saves all mappings
  - Cancel button discards changes
  - Protected FieldMappings property exposes final mapping dictionary

**Implementation Details:**
- Defensive copying of input dictionary prevents external modification
- Dynamic object casting for DataGridView row data
- Two-column layout for efficient data entry and review

### 5. **FieldMapperForm.Designer.cs** (NEW)
Designer file with field mapper UI layout:
- GroupBox for input controls (Add/Remove Mappings section)
- DataGridView for displaying configured mappings (330x200px)
- Proper spacing and alignment
- Form size: 354x350 pixels

### 6. **AppSettings.cs** (UPDATED - referenced)
Application settings extended to support profile management:
- `Profiles`: List<FileFormatProfile> for storing all profiles
- `ActiveProfileName`: String tracking currently selected profile
- Methods for profile lifecycle management

### 7. **ProfileManagementForm.cs** (UPDATED - referenced)
Main UI for managing file format profiles:
- Display list of custom profiles (excludes default)
- Create new profiles
- Edit existing profiles
- Delete profiles (cannot delete default)

### 8. **SettingsForm.cs** (UPDATED - referenced)
Enhanced settings form integration:
- Profile selection dropdown
- "Manage..." button for profile management
- Active profile persistence

### 9. **Form1.cs** (UPDATED - referenced)
Main form integration with profile system:
- Settings button launches SettingsForm with profile support
- Profile selection synchronized with active profile

## User Experience Flow

### Creating a New Profile:
1. Click "Settings" on main form
2. Click "Manage..." in Profile Settings section
3. Click "New" to create new profile
4. ProfileEditForm opens:
   - Enter Profile Name and Description
   - Select Field Delimiter from dropdown (or choose Custom)
   - Select Line Delimiter from dropdown (or choose Custom)
   - Check "First line is header" if applicable
   - Click "Configure Field Mappings..." to add input-to-output mappings
   - FieldMapperForm opens for mapping configuration
   - Save mappings and close FieldMapperForm
   - Click OK to save profile

### Editing a Profile:
1. From ProfileManagementForm, select a profile
2. Click "Edit"
3. ProfileEditForm opens with current settings pre-populated
4. Modify any settings as needed
5. Click "Configure Field Mappings..." to adjust mappings
6. Click OK to save changes

### Configuring Field Mappings:
1. From ProfileEditForm, click "Configure Field Mappings..."
2. FieldMapperForm opens showing current mappings
3. Add new mappings:
   - Enter input field name (e.g., "patient_fname")
   - Enter output property (e.g., "FirstName")
   - Click Add
4. Remove mappings:
   - Select mapping row in DataGridView
   - Click Remove
5. Click OK to save mapping changes

## Data Persistence

- All profiles serialized to JSON in `%APPDATA%\PatientEligibilityTranslator\settings.json`
- Default profile always present and cannot be deleted
- Active profile name saved with settings
- Field mappings stored in each profile
- Special delimiters escaped in JSON (\n, \t, \r)

## Key Features

? **Flexible Delimiters**: Preset options with custom input support  
? **Dynamic UI**: Custom textboxes grey out/show based on selection  
? **Field Mappings**: Add, edit, remove field mappings per profile  
? **Header Support**: Optional first-line header detection  
? **Validation**: Input validation and duplicate prevention  
? **Case-Insensitive**: Input field names normalized for reliable matching  
? **User-Friendly**: Readable labels for line delimiter presets (LF, CRLF, CR)  
? **Defensive Copying**: Prevents unintended data modification  

## Technical Implementation

### Delimiter Handling:
- Field delimiters stored as literal characters (visible in dropdown)
- Line delimiters mapped between user-friendly labels and actual escape sequences
- Custom delimiters support any character combination
- Escape/unescape methods handle special character representation

### Field Mapping:
- Dictionary<string, string> with case-insensitive keys
- Input field names auto-lowercased for consistent matching
- Output properties mapped to patient class properties
- Extensible for future property additions

### Event-Driven UI:
- SelectedIndexChanged events for delimiter dropdowns
- Dynamic control visibility based on user selection
- Auto-focus to custom input when "Custom" selected
- Improved user experience with immediate feedback

## Future Enhancement Opportunities

1. **Column Index Mappings**: UI for column-based format (e.g., CSV with specific column positions)
2. **Profile Import/Export**: Share profile configurations across installations
3. **Format Preview**: Sample file parsing with current profile settings
4. **Field Validation Rules**: Add per-field validation logic
5. **Conditional Mappings**: Support for field transformations and formatting
6. **Profile Templates**: Pre-built profiles for common formats (CSV, XML, FHIR, etc.)
7. **Field Discovery**: Auto-detect available fields from sample input files
8. **Mapping History**: Track and revert mapping changes

## Testing Considerations

- Validate delimiter combinations work correctly in EDI parsing
- Test custom delimiters with special characters
- Verify field mappings applied during file import
- Test header line detection with various formats
- Validate case-insensitive field name matching
- Test profile persistence across application restarts

## Notes

- Built with .NET 9 Windows Forms
- Follows existing code style and patterns
- Minimal dependencies on external libraries
- Maintains backward compatibility with default profile
- All dialogs use parent form centering for consistent UI
