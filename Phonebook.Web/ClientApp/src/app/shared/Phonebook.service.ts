import { Injectable, Inject } from '@angular/core';
import { FormBuilder, Validators, AbstractControl } from '@angular/forms';
import { HttpClient, HttpHeaders, HttpParams } from "@angular/common/http";
import { ContactData, Phonebook } from '../Interfaces/VAInterfaces';

@Injectable({
  providedIn: 'root'
})
export class PhonebookService {

  private static readonly BASE_URL = 'BASE_URL';

  constructor(private formbuilder: FormBuilder, private http: HttpClient, @Inject(PhonebookService.BASE_URL) public baseUrl: string) {
  }

  entryFormModel = this.formbuilder.group({
    Name: ['', Validators.required],
    Number: ['', [Validators.required, this.isValidMobileNumber]],
    PhonebookId: ['']
  });

  phonebookFormModel = this.formbuilder.group({
    Name: ['', Validators.required],
  });

  isValidMobileNumber(mobileNUmber: AbstractControl) {

    let isInvalid = { 'isValidMobileNumber': true };

    if (mobileNUmber.value == "" || mobileNUmber.value == null) {
      return null;
    }

    if (isNaN(mobileNUmber.value)) {
      return isInvalid;
    }

    return (mobileNUmber.value.toString().length != 10) ? isInvalid : null;
  }

  async submit(contactData: ContactData) {
    return await this.saveForm(contactData);
  }

  async savePhonebook(formData: {}) {
    return await this.http.post(this.baseUrl + 'Phonebook/Add', formData, {
      headers: new HttpHeaders({ "Content-Type": "application/json" })
    }).toPromise();
  }

  async saveForm(contactData: ContactData) {
    return await this.http.post(this.baseUrl + 'Phonebook/AddEntry', contactData, {
      headers: new HttpHeaders({ "Content-Type": "application/json" })
    }).toPromise();
  }

  async getEntries(phonebookId: number) {
    return await this.http.get<ContactData[]>(this.baseUrl + 'Phonebook/GetEntries', {
      params: new HttpParams().set('phonebookId', phonebookId.toString())
    }).toPromise();
  }

  async getPhonebooks() {
    return await this.http.get<Phonebook[]>(this.baseUrl + 'Phonebook/Get').toPromise();
  }
}
