import { Component, Inject, OnInit } from '@angular/core';
import { NotificationType, ContactData, Phonebook } from '../Interfaces/VAInterfaces';
import { PhonebookService } from '../shared/Phonebook.service'
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatTableDataSource } from '@angular/material/table';
import { CustomnotificationsService } from '../shared/customnotifications.service';

@Component({
  selector: 'app-Phonebook',
  templateUrl: './Phonebook.component.html',
  styleUrls: ['./Phonebook.component.css']
})
export class PhonebookComponent implements OnInit {

  private readonly submitErrorMessage = 'Something went wrong. Please try again.';
  private readonly submitSuccessMessage = 'Phonebook has been successfully added!';
  DoneLoadingData: boolean = false;
  private contactInfo: ContactData = { name: '', number: null, phonebookId: null };
  private selectedPhonebookId: number;
  tableColumns: string[] = ['contactName', 'contactNumber'];
  entries = new MatTableDataSource<ContactData>();
  Phonebooks: Phonebook[] = [
    { phonebookId: 1, name: "Ronalds clientelle" },
    { phonebookId: 2, name: "Changu's list" },
    { phonebookId: 3, name: "Chisanga's phonebook" },
    { phonebookId: 4, name: "Kunda's book" },
    { phonebookId: 5, name: "Kaoma's notebook" },
    { phonebookId: 6, name: "Make's list of clients" }
  ];

  constructor(public dialog: MatDialog, public phonebookService: PhonebookService, private notificationsService: CustomnotificationsService) { }

  async ngOnInit(): Promise<void> {
    this.phonebookService.entryFormModel.reset();
    this.phonebookService.phonebookFormModel.reset();

    await this.getAllPhonebooks();
    this.DoneLoadingData = true;
  }

  private async getEntries(phonebookId: number) {
    try {
      this.entries.data = await this.phonebookService.getEntries(phonebookId);
      return this.entries.data;
    } catch (err) {
      console.error(err);
    }
  }

  async getAllPhonebooks() {
    try {
      this.Phonebooks = await this.phonebookService.getPhonebooks();
    } catch (err) {
      console.error(err);
    }
  }

  public searchEntries = (value: string) => {
    this.entries.filter = value.trim().toLocaleLowerCase();
  }

  openContactDialog(): void {

    this.contactInfo.phonebookId = this.selectedPhonebookId;

    const dialogRef = this.dialog.open(EntryFormDialogComponent, {
      width: '250px',
      data: this.contactInfo
    });

    dialogRef.afterClosed().subscribe(result => {

      if (result != null && result != undefined) {
        this.entries.data.push(result);
        this.entries = new MatTableDataSource(this.entries.data);
      }

      this.contactInfo = { name: '', number: null, phonebookId: null };
      this.phonebookService.entryFormModel.reset();
    });
  }

  async submitPhonebook(phonebook: Phonebook) {

    try {
      await this.phonebookService.savePhonebook(phonebook);
      await this.getAllPhonebooks();
      this.notificationsService.openSnackBar(this.submitSuccessMessage, 'Dismiss', NotificationType.OK);
      this.phonebookService.phonebookFormModel.reset();
    } catch (err) {
      console.error(err);
      this.notificationsService.openSnackBar(this.submitErrorMessage, 'Dismiss', NotificationType.Error);
    }
  }

  async setPhonebook(phonebook: Phonebook) {

    if (phonebook === undefined) {
      this.entries = new MatTableDataSource(null);
    }
    else {
      this.selectedPhonebookId = phonebook.phonebookId;
      let entries = await this.getEntries(phonebook.phonebookId);
      this.entries = new MatTableDataSource(entries);
    }
  }
}

@Component({
  selector: 'app-EntryForm',
  templateUrl: './EntryFormDialog.component.html',
})
export class EntryFormDialogComponent {

  DisableAddBtn: boolean = false;
  private readonly submitErrorMessage = 'Something went wrong. Please try again.';
  private readonly submitSuccessMessage = 'Contact has been successfully added!';

  constructor(
    public dialogRef: MatDialogRef<EntryFormDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public contactData: ContactData, public phonebookService: PhonebookService,
    private notificationsService: CustomnotificationsService) { }

  onCancelClick(): void {
    this.dialogRef.close();
  }

  async onAddClick(contactData: ContactData): Promise<void> {
    await this.addEntry(contactData);
  }

  private async addEntry(contactData: ContactData) {
    try {
      this.DisableAddBtn = true;
      await this.phonebookService.submit(contactData);
      this.dialogRef.close(contactData);
      this.notificationsService.openSnackBar(this.submitSuccessMessage, 'Dismiss', NotificationType.OK);

    } catch (err) {
      this.notificationsService.openSnackBar(this.submitErrorMessage, 'Dismiss', NotificationType.Error);
      this.DisableAddBtn = false;
      console.error(err);
    }
  }
}
