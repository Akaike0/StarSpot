-----------------------------------------------------------------------------------------------
-- Client Lua Script for MatchQueue
-- Copyright (c) NCsoft. All rights reserved
-----------------------------------------------------------------------------------------------
 
require "Window"
require "GameLib"
require "MatchingGameLib"
require "MatchMakingLib"
 
-----------------------------------------------------------------------------------------------
-- MatchQueue Module Definition
-----------------------------------------------------------------------------------------------
local MatchQueue = {} 
local tLeaveTimer
local tLaunchTimer
 
-----------------------------------------------------------------------------------------------
-- Constants
-----------------------------------------------------------------------------------------------
local tMQ = {	

      		timer = {
	             		default = 1,   
	             		deserter = 0,
	              	},
			bool = {
						value = 0,
					},
			btnbool = {
						value = 0,
					}
		   } 
-----------------------------------------------------------------------------------------------
-- Initialization
-----------------------------------------------------------------------------------------------
function MatchQueue:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self 

    -- initialize variables here

    return o
end

function MatchQueue:Init()
	local bHasConfigureFunction = false
	local strConfigureButtonText = ""
	local tDependencies = {
		-- "UnitOrPackageName",
	}
    Apollo.RegisterAddon(self, bHasConfigureFunction, strConfigureButtonText, tDependencies)
end

-----------------------------------------------------------------------------------------------
-- MatchQueue OnLoad
-----------------------------------------------------------------------------------------------
function MatchQueue:OnLoad()
    -- load our form file
	self.xmlDoc = XmlDoc.CreateFromFile("MatchQueue.xml")
	self.xmlDoc:RegisterCallback("OnDocLoaded", self)
	
	-- Events
	Apollo.RegisterEventHandler("MatchingJoinQueue", "OnJoinQueue", self)
	Apollo.RegisterEventHandler("MatchingLeaveQueue", "OnLeaveQueue", self)
	Apollo.RegisterEventHandler("PVPMatchFinished", "OnMatchFinished", self)
	Apollo.RegisterEventHandler("ButtonSignal", "bClick", self)
	-- Role Check
	Apollo.RegisterEventHandler("MatchingRoleCheckStarted", "OnRoleCheck", self)
	-- Timer
	tLaunchTimer = ApolloTimer.Create(1, true, "OnLaunchTimer", self)
	tLeaveTimer = ApolloTimer.Create(1, false, "OnLeaveTimer", self)
end

-----------------------------------------------------------------------------------------------
-- MatchQueue OnDocLoaded
-----------------------------------------------------------------------------------------------
function MatchQueue:OnDocLoaded()

	if self.xmlDoc ~= nil and self.xmlDoc:IsLoaded() then
	    self.wndMain = Apollo.LoadForm(self.xmlDoc, "MatchQueueForm", nil, self)
		if self.wndMain == nil then
			Apollo.AddAddonErrorText(self, "Could not load the main window for some reason.")
			return
		end
		
	    self.wndMain:Show(true, true)

		-- if the xmlDoc is no longer needed, you should set it to nil
		-- self.xmlDoc = nil
		
		-- Register handlers for events, slash commands and timer, etc.
		-- e.g. Apollo.RegisterEventHandler("KeyDown", "OnKeyDown", self)


		-- Do additional Addon initialization here
	end
end

function MatchQueue:bClick()

	if tMQ.btnbool.value == 0 then
		tMQ.btnbool.value = 1
		self.wndMain:FindChild("bOnoff"):SetText("Off")
		self.wndMain:FindChild("bOnoff"):SetTooltip("Turn it on")
		tLaunchTimer:Stop()
		tLeaveTimer:Stop()
		tMQ.bool.value = 0
		-- MatchingGameLib.LeaveMatchingQueue()
	elseif tMQ.btnbool.value == 1 then
		self.wndMain:FindChild("bOnoff"):SetText("On")
		self.wndMain:FindChild("bOnoff"):SetTooltip("Turn it off")
		tLaunchTimer:Start()
		tLeaveTimer:Stop()
		tMQ.bool.value = 0
		tMQ.btnbool.value = 0
	end
		
end


function MatchQueue:OnRoleCheck()

end

	local keMasterTabs =
	{
		["PvE"]			= 1,
		["PvP"]			= 2,
	}
	

function MatchQueue:OnJoinQueue()
	if tMQ.btnbool.value == 0 then
		self.arMatchesToQueue = MatchMakingLib.GetQueuedEntries()

		tLaunchTimer:Start()
		tMQ.bool.value = 0
		Print("MatchQueen is on! You will automatically leave matches and join the queue")
	end
	
	if not self.tQueueOptions then
	self.tQueueOptions =
	{ 
		[keMasterTabs.PvE] = 
		{
			bVeteran = false,
			bFindOthers = true,
			arRoles = MatchMakingLib.GetEligibleRoles(),
		}, 
		[keMasterTabs.PvP] = 
		{
			bAsMercenary = true,
			arRoles = MatchMakingLib.GetEligibleRoles(),
		},
	}
	end	
end

function MatchQueue:OnLeaveQueue()
		tMQ.bool.value = 0
end

function MatchQueue:OnMatchFinished()
	if MatchingGameLib.IsInGameInstance() then
		MatchingGameLib.LeaveGame()
	end
end

function MatchQueue:OnLaunchTimer()
	if GameLib.IsCharacterLoaded() and tMQ.bool.value == 0 then
		local nActualTimer = tMQ.timer.default 	
		local bIsDeserter = self:IsDeserter() 
		
		if bIsDeserter then
			nActualTimer = tMQ.timer.deserter
		end
		
		tLeaveTimer:Set(nActualTimer, false, "OnLeaveTimer")
		tLeaveTimer:Start()
		tMQ.bool.value = 1
	end
end

function MatchQueue:IsDeserter()
	local uPlayer = GameLib.GetPlayerUnit()  			
	local tBuff = uPlayer:GetBuffs() 	
	local tHarm = tBuff.arHarmful	 	

	for _=1, #tHarm do
      
		if tHarm[_].splEffect:GetId() == 45444 then 
				   
		    tMQ.timer.deserter = tHarm[_].fTimeRemaining    
		 
			return true
		end
	end
	
	return false
end

function MatchQueue:OnLeaveTimer()
	if GameLib.IsCharacterLoaded() and self.arMatchesToQueue then
		self.eSelectedMasterType = keMasterTabs.PvP
		MatchMakingLib.Queue(self.arMatchesToQueue, self.tQueueOptions[self.eSelectedMasterType])
				
		if MatchingGameLib.GetQueueEntry() and not MatchingGameLib.IsFinished() then
			tLeaveTimer:Stop()
			tMQ.bool.value = 0
		end
		
	end
end

-----------------------------------------------------------------------------------------------
-- MatchQueue Instance
-----------------------------------------------------------------------------------------------
local MatchQueueInst = MatchQueue:new()
MatchQueueInst:Init()
