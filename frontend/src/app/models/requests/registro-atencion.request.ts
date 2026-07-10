export interface RegistroAtencionRequest {
  documentoPaciente: string;
  codigoDiagnostico?: string;
  fechaAtencion: string;
  requiereAuditoria: boolean;
}