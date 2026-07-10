import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '@environment/environment';
import { RegistroAtencion } from '@models/registro-atencion.model';
import { RegistroAtencionRequest } from '@models/requests/registro-atencion.request';

@Injectable({
  providedIn: 'root',
})
export class AtencionService {

  private readonly baseUrl = `${environment.apiUrl}/atenciones`;
  private readonly auditoriaUrl = `${environment.apiUrl}/auditoria/atenciones`;

  constructor(private readonly http: HttpClient) {}

  /** Obtiene los datos procesados por la lógica de auditoría */
  getAuditoria(): Observable<RegistroAtencion[]> {
    return this.http.get<RegistroAtencion[]>(this.auditoriaUrl);
  }

  crear(dto: RegistroAtencionRequest): Observable<RegistroAtencion> {
    return this.http.post<RegistroAtencion>(this.baseUrl, dto);
  }

  actualizar(id: number, dto: RegistroAtencionRequest): Observable<RegistroAtencion> {
    return this.http.put<RegistroAtencion>(`${this.baseUrl}/${id}`, dto);
  }

  eliminar(id: number): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`);
  }
}
