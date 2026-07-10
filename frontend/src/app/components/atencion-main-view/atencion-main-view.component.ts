import { Component, OnInit, signal } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { AtencionFormComponent } from '@component/atencion-form/atencion-form.component';
import { AtencionTableComponent } from '@component/atencion-table/atencion-table.component';
import { ConfirmDialogComponent } from '@component/confirm-dialog/confirm-dialog.component';
import { RegistroAtencion } from '@models/registro-atencion.model';
import { RegistroAtencionRequest } from '@models/requests/registro-atencion.request';
import { MaterialSharedModule } from '@modules/material-shared.module';
import { AtencionService } from '@services/atencion/atencion.service';
@Component({
  selector: 'app-atencion-main-view.component',
  templateUrl: './atencion-main-view.component.html',
  styleUrl: './atencion-main-view.component.scss',
  standalone: true,
  imports: [MaterialSharedModule, AtencionFormComponent, AtencionTableComponent],
})
export class AtencionMainViewComponent implements OnInit {
  atenciones: RegistroAtencion[] = [];
  cargando = signal(false);
  atencionSeleccionada: RegistroAtencion | null = null;
  constructor(
    private readonly api: AtencionService,
    private readonly dialog: MatDialog,
    private readonly snackBar: MatSnackBar
  ) {}
  ngOnInit(): void {
    this.cargarAtenciones();
  }
  cargarAtenciones(): void {
    this.cargando.set(true);
    this.api.getAuditoria().subscribe({
  next: (data) => {
    this.atenciones = data;
    this.cargando.set(false);
  },
  error: () => {
    this.snackBar.open('Error al cargar datos', 'Cerrar', { duration: 3000 });
    this.cargando.set(false);
  }
});
  }
  onEditar(atencion: RegistroAtencion): void {
    this.atencionSeleccionada = atencion;
    window.scrollTo({ top: 0, behavior: 'smooth' }); 
  }
  onEliminar(id: number): void {
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      data: { titulo: 'Confirmar Eliminación', mensaje: '¿Está seguro de eliminar este registro?' }
    });
    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.api.eliminar(id).subscribe({
          next: () => {
            this.snackBar.open('Registro eliminado', 'Cerrar', { duration: 2000 });
            this.cargarAtenciones(); 
            if (this.atencionSeleccionada?.idAtencion === id) this.atencionSeleccionada = null;
          },
          error: () => this.snackBar.open('Error al eliminar', 'Cerrar', { duration: 3000 })
        });
      }
    });
  }
  onGuardar(dto: RegistroAtencionRequest): void {
    const accion = this.atencionSeleccionada 
      ? this.api.actualizar(this.atencionSeleccionada.idAtencion, dto)
      : this.api.crear(dto);
    accion.subscribe({
      next: () => {
        this.snackBar.open(this.atencionSeleccionada ? 'Actualizado con éxito' : 'Creado con éxito', 'Cerrar', { duration: 2000 });
        this.atencionSeleccionada = null;
        this.cargarAtenciones(); 
      },
      error: () => this.snackBar.open('Error al guardar', 'Cerrar', { duration: 3000 })
    });
  }
  onCancelarEdicion(): void {
    this.atencionSeleccionada = null;
  }
}
