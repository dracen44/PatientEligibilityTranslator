# Patient Eligibility EDI Translator

A .NET 9 WinForms application for translating patient eligibility data to EDI 270 format and parsing EDI 271 eligibility responses.

## Features

- **Multi-Patient Support**: Process multiple patients in a single batch or as individual requests
- **EDI 270 Translation**: Convert patient information to EDI 270 eligibility inquiry format
- **EDI 271 Parsing**: Read and display EDI 271 eligibility response information in a human-readable format
- **Configurable Settings**: Control batch processing behavior with persistent settings
- **File Management**: Load, save, and manage EDI files

## Getting Started

### Application Structure

```
PatientEligibilityTranslator/
??? Form1.cs/Form1.Designer.cs     # Main application window
??? SettingsForm.cs/.Designer.cs    # Settings dialog window
??? AppSettings.cs                  # Settings management and persistence
??? EDI/
?   ??? EDI270Translator.cs        # EDI 270 generation logic
?   ??? EDI271Translator.cs        # EDI 271 parsing logic
??? Documentation
    ??? EDI270_SETTINGS_GUIDE.md
    ??? README.md (this file)
```

## Using the Application

### Main Window

1. **Select File** - Browse and load a file containing patient data or EDI content
2. **Input Panel** - Enter or view patient information
3. **Output Panel** - View the translated EDI format or parsed results
4. **Translate to 270** - Convert patient data to EDI 270 format
5. **Translate 271** - Parse and display EDI 271 response
6. **Settings** - Configure batch processing options

### Settings Window

Access the Settings dialog by clicking the **Settings** button in the main window.

#### Available Settings

**Single Patient Per Request**
- **Default**: Off (unchecked)
- **When OFF**: Multiple patients are batched together with a single ISA/GS header
- **When ON**: Each patient gets their own complete EDI 270 file with separate headers
- **Use Case**: Turn ON when your system requires independent processing for each patient

**Max Patients/Batch**
- **Default**: 50
- **Description**: Maximum number of patients to include in each batch
- **Active When**: "Single Patient Per Request" is OFF
- **Example**: With 150 patients and Max=50, generates 3 batches of 50 each
- **Validation**: Must be a positive integer (disabled when Single Patient mode is ON)

#### Saving Settings

Settings are automatically saved to:
```
%APPDATA%\PatientEligibilityTranslator\settings.json
```

Settings persist between application sessions.

## Input Format

### Patient Data Format

Separate multiple patients using the `---PATIENT---` delimiter:

```
FirstName: John
LastName: Doe
MemberId: 123456
GroupId: GRP001
DOB: 01/15/1990
PlanHolder: Acme Corp

---PATIENT---

FirstName: Jane
LastName: Smith
MemberId: 123457
GroupId: GRP001
DOB: 05/22/1988
PlanHolder: Acme Corp
```

### Supported Fields

- `FirstName` - Patient's first name
- `LastName` - Patient's last name
- `MemberId` - Insurance member ID
- `GroupId` - Insurance group ID
- `DOB` or `DateOfBirth` - Date of birth (flexible format: MM/DD/YYYY, MM-DD-YYYY, MMDDYYYY, CCYYMMDD)
- `PlanHolder` - Name of the plan holder/employer
- `Relationship` - Relationship to plan holder (optional)

## Output Formats

### EDI 270 Output

Generates standard EDI 270 (Health Care Eligibility/Benefit Inquiry) format with:
- ISA (Interchange Control Header)
- GS (Functional Group Header)
- ST (Transaction Set Header) - one per patient
- BHT (Beginning of Hierarchical Transaction)
- HL (Hierarchical Level)
- NM1 (Name segments)
- DMG (Demographic Information)
- REF (Reference Identification)
- SE (Transaction Set Trailer)
- GE (Functional Group Trailer)
- IEA (Interchange Control Trailer)

### EDI 271 Output

Parses and displays:
- Interchange and Group headers
- Patient/Member information
- Demographics
- Eligibility/Benefit status
- Service type information
- Validation status and messages

## Processing Examples

### Example 1: Batch Processing (Default)
- **Input**: 3 patients
- **Settings**: Single Patient OFF, Max=50
- **Output**: 1 EDI batch with 3 transaction sets (ST/SE pairs)

### Example 2: Individual Processing
- **Input**: 3 patients
- **Settings**: Single Patient ON
- **Output**: 3 complete EDI 270 files concatenated

### Example 3: Large Batch with Limits
- **Input**: 150 patients
- **Settings**: Single Patient OFF, Max=50
- **Output**: 3 EDI batches (50, 50, 50 patients each)

## Requirements

- .NET 9.0 or later
- Windows Forms support
- System.Text.Json for settings serialization

## Building

```powershell
dotnet build
```

## Running

```powershell
dotnet run
```

## Architecture

### EDI270Translator Class

**Public Properties:**
- `SinglePatientPerRequest` - Controls batch generation mode
- `MaxPatientsPerBatch` - Sets batch size limit

**Public Methods:**
- `Translate(string inputData)` - Translates patient data to EDI 270 format

**Private Methods:**
- `ParseMultiplePatients()` - Extracts patient records from input
- `GenerateSinglePatientBatches()` - Creates individual EDI files
- `GenerateMultiPatientBatches()` - Creates batched EDI files
- `BuildEDI270Batch()` - Generates EDI 270 segments

### AppSettings Class

**Purpose:** Manages application settings with persistent storage

**Features:**
- Auto-loads settings from AppData on startup
- Saves settings to JSON file
- Provides default values
- Graceful error handling

**Storage Location:**
```
%APPDATA%\PatientEligibilityTranslator\settings.json
```

### SettingsForm Class

**Purpose:** Modal dialog for configuring application settings

**Features:**
- Loads current settings on open
- Validates input values
- Disables/enables fields based on context
- Returns updated settings on OK

## Error Handling

- Invalid file paths show error dialogs
- Date format errors fall back to current date
- Settings validation prevents invalid configurations
- File I/O errors are handled gracefully with debug output

## License

[Specify your license here]

## Contributing

[Contributing guidelines if applicable]

## Support

For issues or questions, please refer to the EDI270_SETTINGS_GUIDE.md for detailed settings documentation.
