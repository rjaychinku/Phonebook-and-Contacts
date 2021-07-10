import { Injectable, Inject } from '@angular/core';
import { FormBuilder, Validators, AbstractControl } from '@angular/forms';
import { HttpClient, HttpHeaders, HttpParams } from "@angular/common/http";
import { ContactData } from '../Interfaces/VAInterfaces';

@Injectable({
  providedIn: 'root'
})
export class PhonebookService {

  private static readonly BASE_URL = 'BASE_URL';

  constructor(private formbuilder: FormBuilder, private http: HttpClient, @Inject(PhonebookService.BASE_URL) public baseUrl: string) {
  }

  entryFormModel = this.formbuilder.group({
    Name: ['', Validators.required],
    Number: ['', [Validators.required, this.isValidMobileNumber]]
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

  async submit() {
    return await this.saveForm(this.entryFormModel.value);
  }

  async saveForm(formData: {}) {
    return await this.http.post(this.baseUrl + 'Phonebook/AddEntry', formData, {
      headers: new HttpHeaders({ "Content-Type": "application/json" })
    }).toPromise();
  }

  async getEntries(phonebookId: number) {
    return await this.http.get<ContactData[]>(this.baseUrl + 'Phonebook/GetEntries', {
      params: new HttpParams().set('phonebookId', phonebookId.toString())
    }).toPromise();
  }
}
