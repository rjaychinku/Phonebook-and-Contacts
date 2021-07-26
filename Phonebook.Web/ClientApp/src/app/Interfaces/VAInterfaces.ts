
export interface ContactData {
  name: string;
  number: number;
  phonebookId: number;
}

export interface Phonebook {
  phonebookId: number;
  name: string;
}

  export enum NotificationType {
    OK = 1,
    Error = 2
  }
