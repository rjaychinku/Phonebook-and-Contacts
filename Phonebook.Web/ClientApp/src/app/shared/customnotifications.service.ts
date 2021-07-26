import { Injectable } from '@angular/core';
import { MatSnackBar, MatSnackBarHorizontalPosition, MatSnackBarVerticalPosition } from '@angular/material/snack-bar';
import { NotificationType } from '../Interfaces/VAInterfaces';

@Injectable({
  providedIn: 'root'
})

export class CustomnotificationsService {
  private horizontalPosition: MatSnackBarHorizontalPosition = 'center';
  private verticalPosition: MatSnackBarVerticalPosition = 'bottom';
  private NotificationDuration: number = 5000;

  constructor(private snackBar: MatSnackBar) { }

   openSnackBar(message: string, dismissMessage: string, notificationType: number) {
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

}
