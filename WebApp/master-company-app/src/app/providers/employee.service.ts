import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { EmployeeDTO } from '../DTOs/employee.DTO';
import { ServicesResult } from '../DTOs/services-result';

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {

  constructor(private readonly httpClient: HttpClient) { }

  getAll() : Observable<ServicesResult<EmployeeDTO[]>> {
    return this.httpClient.get<ServicesResult<EmployeeDTO[]>>(`${environment.apiUrl}api/Employee`);
  }
}
