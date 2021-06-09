import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MatDialogModule } from '@angular/material/dialog';
import { MatTableModule } from '@angular/material/table';

@NgModule({
  declarations: [],
  imports: [
    MatTableModule,
    MatDialogModule,
    CommonModule,
    MatInputModule,
    MatButtonModule, 
    MatSnackBarModule,
  ], exports: [
    MatTableModule,
    MatDialogModule,
    MatInputModule,
    MatButtonModule,
    MatSnackBarModule,
  ]
})
export class AppAngMaterialModule { }
