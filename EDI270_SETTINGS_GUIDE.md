# EDI 270 Translator Settings Guide

## Overview
The Patient Eligibility EDI Translator now supports configurable batch processing settings for generating EDI 270 eligibility inquiry files.

## Settings

### 1. Single Patient Per Request
**Default:** Unchecked (False)

- **When Unchecked (False):**
  - Multiple patients are processed in a single EDI 270 batch
  - Each patient gets their own transaction set (ST/SE pair) within one batch
  - Respects the "Max Patients/Batch" setting
  - More efficient for bulk processing

- **When Checked (True):**
  - Each patient generates a separate, complete EDI 270 batch
  - Results in multiple standalone EDI 270 files concatenated together
  - Each patient gets their own ISA/GS header and IEA/GE trailer
  - Useful when each patient needs independent processing

### 2. Max Patients/Batch
**Default:** 50

- **Applies when:** "Single Patient Per Request" is unchecked
- **Purpose:** Limits the number of patients in each batch
- **Example:** 
  - If you have 150 patients and Max Patients/Batch = 50
  - The output will contain 3 separate batches of 50, 50, and 50 patients
- **Rules:**
  - Must be a positive integer
  - Invalid entries (0 or negative) will show an error
  - Field is disabled when "Single Patient Per Request" is enabled

## Input Format

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

---PATIENT---

FirstName: Robert
LastName: Johnson
MemberId: 123458
GroupId: GRP001
DOB: 03/10/1985
PlanHolder: Acme Corp
```

## Example Scenarios

### Scenario 1: 3 Patients, Multi-Patient Mode (Default)
- SinglePatientPerRequest: False
- MaxPatientsPerBatch: 50
- **Result:** 1 batch with 3 transactions (ST/SE pairs)

### Scenario 2: 3 Patients, Single Patient Mode
- SinglePatientPerRequest: True
- MaxPatientsPerBatch: 50 (ignored)
- **Result:** 3 separate EDI 270 files concatenated

### Scenario 3: 150 Patients, Multi-Patient Mode, Limit 50/Batch
- SinglePatientPerRequest: False
- MaxPatientsPerBatch: 50
- **Result:** 3 batches - Batch1 (50 patients), Batch2 (50 patients), Batch3 (50 patients)

## Output Structure

### Multi-Patient Batch (Default)
```
ISA* ... (single header)
GS*  ... (single group header)
  ST* ... (Patient 1)
  ...data...
  SE* ... (Patient 1 end)
  
  ST* ... (Patient 2)
  ...data...
  SE* ... (Patient 2 end)
  
  ST* ... (Patient 3)
  ...data...
  SE* ... (Patient 3 end)
GE*  ... (counts transactions: 3)
IEA* ... (counts groups: 1)
```

### Single Patient Mode
```
ISA* ... (Batch 1)
GS*  ...
  ST* ... (Patient 1)
  ...data...
  SE* ...
GE*  ...
IEA* ...

ISA* ... (Batch 2)
GS*  ...
  ST* ... (Patient 2)
  ...data...
  SE* ...
GE*  ...
IEA* ...

ISA* ... (Batch 3)
GS*  ...
  ST* ... (Patient 3)
  ...data...
  SE* ...
GE*  ...
IEA* ...
```
