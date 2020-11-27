import React, { useState } from 'react';
import {
  Collapse,
  Navbar,
  NavbarToggler,
  NavbarBrand,
  Nav,
  NavItem,
  NavLink,
  UncontrolledDropdown,
  DropdownToggle,
  DropdownMenu,
  DropdownItem,
  NavbarText
} from 'reactstrap';

const MeyerNavbar = (props) => {
  const [isOpen, setIsOpen] = useState(false);

  const toggle = () => setIsOpen(!isOpen);

  return (
    <div>
      <Navbar color="light" light expand="md">
        <NavbarBrand href="/">Meyer Logging</NavbarBrand>
        <NavbarToggler onClick={toggle} />
        <Collapse isOpen={isOpen} navbar>
          <Nav className="mr-auto" navbar>
            <UncontrolledDropdown nav inNavbar>
              <DropdownToggle nav caret>
                Applications
              </DropdownToggle>
              <DropdownMenu right>
                <DropdownItem>
                  All
                </DropdownItem>
                <DropdownItem divider />
                <DropdownItem>
                  CER
                </DropdownItem>
                <DropdownItem>
                  Accounts Payable
                </DropdownItem>
                <DropdownItem>
                  EDI
                </DropdownItem>
              </DropdownMenu>
            </UncontrolledDropdown>
            <UncontrolledDropdown nav inNavbar>
              <DropdownToggle nav caret>
                User
              </DropdownToggle>
              <DropdownMenu right>
                <DropdownItem>
                  All
                </DropdownItem>
                <DropdownItem divider />
                <DropdownItem>
                  CER
                </DropdownItem>
                <DropdownItem>
                  Accounts Payable
                </DropdownItem>
                <DropdownItem>
                  EDI
                </DropdownItem>
              </DropdownMenu>
            </UncontrolledDropdown>
            <UncontrolledDropdown nav inNavbar>
              <DropdownToggle nav caret>
                Severity
              </DropdownToggle>
              <DropdownMenu right>
                <DropdownItem>
                  All
                </DropdownItem>
                <DropdownItem divider />
                <DropdownItem>
                  CER
                </DropdownItem>
                <DropdownItem>
                  Accounts Payable
                </DropdownItem>
                <DropdownItem>
                  EDI
                </DropdownItem>
              </DropdownMenu>
            </UncontrolledDropdown>
          </Nav>
          <Nav className="mr-auto" navbar>
            <NavItem>
              <NavLink href="/about/">About</NavLink>
            </NavItem>
            <NavItem>
              <NavLink href="/help/">Help</NavLink>
            </NavItem>
            <NavItem>
              <NavLink href="/other/">Other Applications</NavLink>
            </NavItem>
          </Nav>
          <Nav className="mr-auto" navbar>
            <NavItem>
              <UncontrolledDropdown nav inNavbar>
                <DropdownToggle nav caret>
                  Daniel Przybylski
                </DropdownToggle>
                <DropdownMenu right>
                  <DropdownItem>
                    Development
                </DropdownItem>
                  <DropdownItem>
                    Test
                </DropdownItem>
                  <DropdownItem>
                    Production
                </DropdownItem>
                  <DropdownItem divider />
                  <DropdownItem>
                    Reset
                </DropdownItem>
                </DropdownMenu>
              </UncontrolledDropdown>
            </NavItem>
          </Nav>
        </Collapse>
      </Navbar>
    </div>
  );
}

export default MeyerNavbar;