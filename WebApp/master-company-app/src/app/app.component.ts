import { Component, OnInit } from '@angular/core';
import { EmployeeDTO } from './DTOs/employee.DTO';
import { EmployeeService } from './providers/employee.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{
  public employees!: EmployeeDTO[];
  public errorMessage!: string;

  constructor(private readonly employeeService: EmployeeService) { }

  ngOnInit(): void {
    this.employeeService.getAll()
        .subscribe((result) => {
          if (result.executedSuccessfully) {
            this.errorMessage = '';
            this.employees = result.data;
            return;
          }

          this.errorMessage = result.message;
        });
  }
}
