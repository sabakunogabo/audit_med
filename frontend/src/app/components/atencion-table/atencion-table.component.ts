import { Component, EventEmitter, Input, Output } from '@angular/core';
import { RegistroAtencion } from '@models/registro-atencion.model';
import { MaterialSharedModule } from '@modules/material-shared.module';
import { MiscellaneousModule } from '@modules/micellaneous.module';

@Component({
  selector: 'app-atencion-table',
  templateUrl: './atencion-table.component.html',
  styleUrl: './atencion-table.component.scss',
  standalone: true,
  imports: [MiscellaneousModule, MaterialSharedModule],
})
export class AtencionTableComponent {
  @Input() atenciones: RegistroAtencion[] = [];
  @Input() cargando: boolean = false;
  
  @Output() editarEvent = new EventEmitter<RegistroAtencion>();
  @Output() eliminarEvent = new EventEmitter<number>();

  columnasMostradas: string[] = ['idAtencion', 'documentoPaciente', 'codigoDiagnostico', 'fechaAtencion', 'requiereAuditoria', 'acciones'];

  formatearFecha(fechaIso: string): string {
    return new Date(fechaIso).toLocaleDateString('es-CO', { day: '2-digit', month: '2-digit', year: 'numeric' });
  }
  
  getRowClass(atencion: RegistroAtencion): string {
    return atencion.requiereAuditoria ? 'fila-auditoria' : '';
  }
}
