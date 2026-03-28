; RNG table reads
org $83A3AA : JSL RandomL
org $83A3E9 : JSL RandomL
org $83A9E2 : JSL RandomL
org $83B24E : JSR Random
org $83B275 : JSR Random

; IncrementalRandom calls
org $808FD8 : JSL RandomL
org $82A746 : JSL RandomL
org $82A7CF : JSL RandomL
org $83A427 : JSL RandomL
org $83A45A : JSL RandomL
org $83A4D4 : JSL RandomL
org $83A4F9 : JSL RandomL
org $83B7F5 : JSL RandomL
org $83B8F6 : JSL RandomL

org $83DA00

Random:
	JSL RandomL
	RTS

;===================================================================================================

RandomL:
	PHP

	REP #$23

	LDA.w SeedWRAM+0
	ASL
	ROL
	ROL
	ROL
	ROL
	EOR.w SeedWRAM+0
	STA.l SeedSRAM

	LDA.w SeedWRAM+2
	STA.w SeedWRAM+0

	LDA.l SeedSRAM
	LSR
	ROR
	ROR
	EOR.l SeedSRAM
	STA.l SeedSRAM

	LDA.w SeedWRAM+2
	ROR
	EOR.w SeedWRAM+2
	EOR.l SeedSRAM
	STA.w SeedWRAM+2

	PLP

	RTL

;===================================================================================================
