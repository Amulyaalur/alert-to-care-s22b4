# Alert to Care

This project implements a service that receives
measurements from devices that monitor patients.
It's part of a solution that provides care in an ICU.

It has two segments:

1. The API - Both GUI and non-GUI clients can consume this.
1. The GUI, which is one client of the API

About the clients:

- GUI-clients are for standalone use of this product.
- Non-GUI clients are other systems that need to integrate with this product.

This project implements automated tests, which behave as a non-GUI client.

## [Segment 1] Web API

The product API covers the following user-stories.

### Configuration

A setup-client needs to configure an ICU.
Context: This configuration relates the alerts to a patient-bed

Acceptance criteria: The configuration needs to include:

- Number of beds
- Bed-identification
- Layout information

### Occupancy Interface

An occupancy-management-client needs to set the status of each bed

- Occupancy of each bed
- Enter Patient details
- Discharging a patient

### Monitoring

A monitoring-client needs to act on alerts
Subscribers can be a GUI, an SMS service, a mobile app, etc.

Provide a facility to turn off alarms

### REST API guidelines

- [Introductory guide](https://stackoverflow.blog/2020/03/02/best-practices-for-rest-api-design/)
- [Microsoft REST API guideline](https://github.com/microsoft/api-guidelines/blob/vNext/Guidelines.md)

## [Segment 2] User-interface

### User-story: Setup

As Operations, I need to setup the ICU alert-service and its interface

Example: The hospital has an ICU with 12 beds. I need to setup the server,
install the software and configure the number and layout of the beds.

Acceptance Criteria:

- The setup can have a maximum of 4 manual steps.
All manual steps shall be at the beginning or the end of installation.
(Manual steps in the middle of installation are confusing for operations)
- Errors during the setup shall produce logs.
These logs will lead to repair-actions by Operations.
- The layouts provided shall cater to a minimum of two common ICU layouts

### User-story: Manage Occupancy

As a nurse, I need to set the occupancy of beds

Example: When admission or discharge occurs,
I need a GUI to mark the bed

Acceptance Criteria:

- There shall be no alerts shown from empty beds.
Don’t assume that the patient monitor has a network-interface to shut it down.
- Admission and discharge shall be possible with a maximum of 3 clicks.

### User-story: Respond to care

As a nurse, I need to manage and respond to alerts

Example: After I’ve handled an alert of a patient’s parameter,
I need to reset it on the GUI

Acceptance Criteria:

- Resetting an alert of a patient’s parameter shall be possible
with a single click
- It shall be possible to undo the reset
(a single click can be a mistake)
- Optional: Patient-specific alerts:
It shall be possible to configure the thresholds per patient
