import { Component, EventEmitter, Input, OnDestroy, OnInit, Output } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { RegistroAtencion } from '@models/registro-atencion.model';
import { RegistroAtencionRequest } from '@models/requests/registro-atencion.request';
import { MaterialSharedModule } from '@modules/material-shared.module';
import { MiscellaneousModule } from '@modules/micellaneous.module';
import { Subscription } from 'rxjs';
@Component({
  selector: 'app-atencion-form',
  templateUrl: './atencion-form.component.html',
  styleUrl: './atencion-form.component.scss',
  standalone: true,
  imports: [MaterialSharedModule, MiscellaneousModule],
})
export class AtencionFormComponent implements OnInit, OnDestroy {
  @Input() atencionParaEditar: RegistroAtencion | null = null;
  @Output() guardarEvent = new EventEmitter<RegistroAtencionRequest>();
  @Output() cancelarEvent = new EventEmitter<void>();
  formulario: FormGroup;
  modoEdicion = false;
  private sub?: Subscription;
  constructor(private readonly fb: FormBuilder) {
    this.formulario = this.fb.group({
      documentoPaciente: ['', [Validators.required, Validators.minLength(5), Validators.maxLength(20)]],
      codigoDiagnostico: [''],
      fechaAtencion: [new Date().toISOString().split('T')[0], [Validators.required]],
      requiereAuditoria: [false]
    });
  }
  ngOnInit(): void {
    this.sub = this.formulario.valueChanges.subscribe(() => {
      this.formulario.markAsDirty();
    });
  }
  ngOnChanges(): void {
    if (this.atencionParaEditar) {
      this.modoEdicion = true;
      this.formulario.patchValue({
        documentoPaciente: this.atencionParaEditar.documentoPaciente,
        codigoDiagnostico: this.atencionParaEditar.codigoDiagnostico || '',
        fechaAtencion: new Date(this.atencionParaEditar.fechaAtencion).toISOString().split('T')[0],
        requiereAuditoria: this.atencionParaEditar.requiereAuditoria
      });
    } else {
      this.modoEdicion = false;
      this.limpiar();
    }
  }
  ngOnDestroy(): void { this.sub?.unsubscribe(); }
  onSubmit(): void {
    if (this.formulario.invalid) {
      this.formulario.markAllAsTouched();
      return;
    }
    this.guardarEvent.emit(this.formulario.value as RegistroAtencionRequest);
    this.limpiar();
  }
  limpiar(): void {
    this.formulario.reset({ 
      documentoPaciente: '', 
      codigoDiagnostico: '', 
      fechaAtencion: new Date().toISOString().split('T')[0], 
      requiereAuditoria: false 
    });
    this.modoEdicion = false;
  }
  cancelar(): void {
    this.limpiar();
    this.cancelarEvent.emit();
  }
  get f() { return this.formulario.controls; }
}
