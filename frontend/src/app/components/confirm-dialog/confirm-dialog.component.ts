import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MaterialSharedModule } from '@modules/material-shared.module';

@Component({
  selector: 'app-confirm-dialog.component',
  templateUrl: './confirm-dialog.component.html',
  styleUrl: './confirm-dialog.component.scss',
  standalone: true,
  imports: [MaterialSharedModule],
})
export class ConfirmDialogComponent {
  constructor(
    public dialogRef: MatDialogRef<ConfirmDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: { titulo: string, mensaje: string }
  ) {}

  confirmar(): void { this.dialogRef.close(true); }
  cancelar(): void { this.dialogRef.close(false); }
}
