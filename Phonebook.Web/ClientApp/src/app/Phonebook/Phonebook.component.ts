import { Component, Inject, OnInit } from '@angular/core';
import { Validators } from '@angular/forms';
import { MatSnackBar, MatSnackBarHorizontalPosition,  MatSnackBarVerticalPosition } from '@angular/material/snack-bar';
import { NotificationType, ContactData, Phonebook } from '../Interfaces/VAInterfaces';
import { PhonebookService } from '../shared/Phonebook.service'
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatTableDataSource } from '@angular/material/table';

@Component({
  selector: 'app-Phonebook',
  templateUrl: './Phonebook.component.html',
  styleUrls: ['./Phonebook.component.css']
})
export class PhonebookComponent implements OnInit {

  DoneLoadingData: boolean = false;
  contactInfo: ContactData = { name: '', number: null };
  phonebookId: number = 1; //using a fixed one to mock an actual device?
  tableColumns: string[] = ['contactName', 'contactNumber'];
  entries = new MatTableDataSource<ContactData>();
  Phonebooks: Phonebook[] = [
    { id: 1, name: "Ronalds clientelle" },
    { id: 2, name: "Changu's list" },
    { id: 3, name: "Chisanga's phonebook" },
    { id: 4, name: "Kunda's clientelle" },
    { id: 5, name: "Kaoma's clientelle" },
    { id: 6, name: "Make's clientelle" }
  ];

  constructor(public dialog: MatDialog, public phonebookService: PhonebookService) { }

  async ngOnInit(): Promise<void> {
    this.phonebookService.entryFormModel.reset();
    await this.getAllEntries();
    this.DoneLoadingData = true;
  }

  async getAllEntries() {
    try {
      let rr = this.Phonebooks.length === 6;
      this.entries.data = await this.phonebookService.getEntries(this.phonebookId);
    } catch (err) {
      console.error(err);
    }
  }

  public searchEntries = (value: string) => {
    this.entries.filter = value.trim().toLocaleLowerCase();
  }

  openContactDialog(): void {
    const dialogRef = this.dialog.open(EntryFormDialogComponent, {
      width: '250px',
      data: this.contactInfo
    });

    dialogRef.afterClosed().subscribe(result => {

      if (result != null && result != undefined) {
        this.entries.data.push(result);
        this.entries = new MatTableDataSource(this.entries.data);
      }

      this.contactInfo = { name: '', number: null };
      this.phonebookService.entryFormModel.reset();
    });
  }

  openPhonebookDialog(): void {
    const dialogRef = this.dialog.open(EntryFormDialogComponent, {
      width: '250px',
      data: this.contactInfo
    });

    dialogRef.afterClosed().subscribe(result => {

      if (result != null && result != undefined) {
        this.entries.data.push(result);
        this.entries = new MatTableDataSource(this.entries.data);
      }

      this.phonebookService.phonebookFormModel.reset();
    });
  }

  setPhonebook(event: any) {
  }

}

@Component({
  selector: 'app-EntryForm',
  templateUrl: './EntryFormDialog.component.html',
})
export class EntryFormDialogComponent {
  horizontalPosition: MatSnackBarHorizontalPosition = 'center';
  verticalPosition: MatSnackBarVerticalPosition = 'bottom';
  NotificationDuration: number = 5000;
  DisableAddBtn: boolean = false;

  private readonly submitErrorMessage = 'Something went wrong. Please try again.';
  private readonly submitSuccessMessage = 'Contact has been successfully added!';

  constructor(
    public dialogRef: MatDialogRef<EntryFormDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public contactData: ContactData, public phonebookService: PhonebookService, private snackBar: MatSnackBar) { }

  onCancelClick(): void {
    this.dialogRef.close();
  }

  async onAddClick(contactData: ContactData): Promise<void> {
    await this.addEntry(contactData);
  }

  private openSnackBar(message: string, dismissMessage: string, notificationType: number) {
    let displayTheme = 'green-snackbar';

    if (NotificationType.Error == notificationType) {
      displayTheme = 'red-snackbar';
    }

    this.snackBar.open(message, dismissMessage, {
      duration: this.NotificationDuration,
      panelClass: [displayTheme],
      horizontalPosition: this.horizontalPosition,
      verticalPosition: this.verticalPosition,
    });
  }

  private async addEntry(contactData: ContactData) {
    try {
      this.DisableAddBtn = true;
      const response = await this.phonebookService.submit();

      if (response == true) {
        this.dialogRef.close(contactData);
        this.openSnackBar(this.submitSuccessMessage, 'Dismiss', NotificationType.OK);
      }
      else {
        this.openSnackBar(this.submitErrorMessage, 'Dismiss', NotificationType.Error);
        this.DisableAddBtn = false;
      }
    } catch (err) {
      this.openSnackBar(this.submitErrorMessage, 'Dismiss', NotificationType.Error);
      this.DisableAddBtn = false;
      console.error(err);
    }
  }
}
