export interface RegistroAtencion {
  idAtencion: number;
  documentoPaciente: string;
  codigoDiagnostico: string | null;
  fechaAtencion: string;
  requiereAuditoria: boolean;
}